# Script de Simulación Simple - Sport Event Manager
# Simula corredores llegando a la meta en tiempo real 

$baseUrl = "http://localhost:5221"
$endpoint = "$baseUrl/api/timing/register"

# Configuración de la carrera 
# Ajustar según la base de datos
$raceId = 1
$penultimateSplitId = 1002
$finishSplitId = 2

# Corredores a simular (ChipIds)
$runners = @(1, 2, 5, 6, 9)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  SIMULADOR SIMPLE - FINISH LINE" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Race ID: $raceId"
Write-Host "Splits: $penultimateSplitId (Aprox) -> $finishSplitId (Meta)"
Write-Host "Hora actual: $(Get-Date)"

function Send-ChipReading {
    param(
        [int]$ChipId,
        [int]$SplitId
    )
    
    $timestamp = (Get-Date).ToUniversalTime()
    $body = @{
        ChipId    = $ChipId
        SplitId   = $SplitId
        Timestamp = $timestamp.ToString("yyyy-MM-ddTHH:mm:ss")
    } | ConvertTo-Json

    try {
        # Ignorar SSL
        if (-not ([System.Management.Automation.PSTypeName]'ServerCertificateValidationCallback').Type) {
            $certCallback = @"
                using System;
                using System.Net;
                using System.Net.Security;
                using System.Security.Cryptography.X509Certificates;
                public class ServerCertificateValidationCallback {
                    public static void Ignore() {
                        if(ServicePointManager.ServerCertificateValidationCallback == null) {
                            ServicePointManager.ServerCertificateValidationCallback += 
                                delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {
                                    return true;
                                };
                        }
                    }
                }
"@
            Add-Type $certCallback
        }
        [ServerCertificateValidationCallback]::Ignore()

        Invoke-RestMethod -Uri $endpoint -Method Post -Body $body -ContentType "application/json" -ErrorAction Stop | Out-Null
        
        $timeStr = $timestamp.ToString('HH:mm:ss')
        Write-Host "[$timeStr] Chip $ChipId -> Split $SplitId (Enviado)" -ForegroundColor Green
        return $true
    }
    catch {
        Write-Host "[ERROR] Chip ${ChipId}: $($_.Exception.Message)" -ForegroundColor Red
        return $false
    }
}

Write-Host "Presione ENTER para comenzar..." -ForegroundColor Yellow
Read-Host

foreach ($chipId in $runners) {
    Write-Host "----------------------------------------"
    Write-Host "Corredor #$chipId" -ForegroundColor Magenta
    
    # 1. Aproximación
    Send-ChipReading -ChipId $chipId -SplitId $penultimateSplitId
    
    # Esperar un poco para que se vea en pantalla
    Start-Sleep -Seconds 4
    
    # 2. Llegada
    Send-ChipReading -ChipId $chipId -SplitId $finishSplitId
    
    # Esperar al siguiente corredor
    Start-Sleep -Seconds 3
}

Write-Host ""
Write-Host "Fin." -ForegroundColor Cyan
