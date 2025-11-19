Param(
  [string]$Root = (Get-Location).Path,
  [string[]]$Extensions = @(
    "*.cs","*.razor","*.csproj","*.sln",
    "*.json","*.yml","*.yaml",
    "*.md","*.scss","*.css",
    "*.js","*.ts","*.ps1","*.cshtml"
  ),
  [switch]$WithBom,          # Si se pasa, genera UTF-8 con BOM; por defecto sin BOM
  [switch]$DryRun,          # Muestra lo que haría sin escribir cambios
  [switch]$Backup           # Crea copia .bak antes de convertir
)

# Carpetas a excluir
$ExcludeDirs = @("\.git\", "\.vs\", "\bin\", "\obj\", "\node_modules\")

# UTF-8 destino
$targetEncoding = New-Object System.Text.UTF8Encoding($WithBom.IsPresent)

# Preferencias de salida
$wantBom  = $WithBom.IsPresent
$bomLabel = if ($wantBom) { " (BOM)" } else { "" }

# Encuentra archivos candidatos
function Get-TextFiles {
  Get-ChildItem -Path $Root -Recurse -File -Include $Extensions |
    Where-Object {
      $full = $_.FullName
      -not ($ExcludeDirs | ForEach-Object { $full -match [regex]::Escape($_) } | Where-Object { $_ })
    }
}

$converted = 0
$skipped   = 0
$failed    = 0

foreach ($file in Get-TextFiles) {
  try {
    # Lee detectando BOM si existe
    $fs = [System.IO.File]::Open($file.FullName, [System.IO.FileMode]::Open, [System.IO.FileAccess]::Read, [System.IO.FileShare]::Read)
    try {
      $reader   = New-Object System.IO.StreamReader($fs, $true) # detectEncodingFromByteOrderMarks = true
      $content  = $reader.ReadToEnd()
      $encoding = $reader.CurrentEncoding
      $reader.Close()
    } finally {
      $fs.Close()
    }

    # Si ya es UTF-8 y el estado de BOM coincide, omite
    $isUtf8 = $encoding.WebName -eq "utf-8"
    $hasBom = $encoding.GetPreamble().Length -gt 0

    if ($isUtf8 -and ($hasBom -eq $wantBom)) {
      $skipped++
      continue
    }

    # Backup opcional
    if ($Backup.IsPresent -and -not $DryRun.IsPresent) {
      Copy-Item -Path $file.FullName -Destination ($file.FullName + ".bak") -Force
    }

    if ($DryRun.IsPresent) {
      Write-Host "[DRY] Convertir -> UTF-8$bomLabel : $($file.FullName)"
    } else {
      [System.IO.File]::WriteAllText($file.FullName, $content, $targetEncoding)
      Write-Host "OK   -> UTF-8$bomLabel : $($file.FullName)"
    }

    $converted++
  }
  catch {
    $failed++
    Write-Warning "Fallo al convertir: $($file.FullName) - $($_.Exception.Message)"
  }
}

Write-Host "`nResumen: convertidos=$converted, omitidos=$skipped, errores=$failed"
