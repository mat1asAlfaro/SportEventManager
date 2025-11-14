# Script de Simulación de Carrera - Sport Event Manager
# Este script simula lecturas de chips RFID para todas las carreras
$baseUrl = "http://localhost:5221"  # Puerto HTTP de desarrollo
$endpoint = "$baseUrl/api/timing/register"

# Configuración de carreras
$races = @(
    @{
        RaceId = 1
        Name = "Carrera 1"
        Splits = @(1, 2)
        Runners = @(1, 2, 5, 6, 9, 10)  # ChipIds de corredores en carrera 1
    },
    @{
        RaceId = 2
        Name = "Carrera 2"
        Splits = @(3, 4)
        Runners = @(3, 4, 7, 8)  # ChipIds de corredores en carrera 2
    }
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  SIMULADOR DE CARRERA - RFID TIMING" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Función para enviar lectura de chip
function Send-ChipReading {
    param(
        [int]$ChipId,
        [int]$SplitId,
        [datetime]$Timestamp
    )
    
    $body = @{
        ChipId = $ChipId
        SplitId = $SplitId
        Timestamp = $Timestamp.ToString("yyyy-MM-ddTHH:mm:ss")
    } | ConvertTo-Json

    try {
        # Ignorar errores de certificado SSL en desarrollo
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
                                delegate(
                                    Object obj, 
                                    X509Certificate certificate, 
                                    X509Chain chain, 
                                    SslPolicyErrors errors
                                ) {
                                    return true;
                                };
                        }
                    }
                }
"@
            Add-Type $certCallback
        }
        [ServerCertificateValidationCallback]::Ignore()

        $response = Invoke-RestMethod -Uri $endpoint -Method Post -Body $body -ContentType "application/json" -ErrorAction Stop
        
        $timeStr = $Timestamp.ToString('HH:mm:ss')
        Write-Host "[OK] Chip $ChipId | Split $SplitId | $timeStr | TimeRecord ID: $($response.timeRecordId)" -ForegroundColor Green
        
        return $true
    }
    catch {
        Write-Host "[ERROR] Chip $ChipId | Split $SplitId | Error: $($_.Exception.Message)" -ForegroundColor Red
        return $false
    }
}

# Preguntar al usuario el modo de simulación
Write-Host "Seleccione el modo de simulacion:" -ForegroundColor Yellow
Write-Host "1. Simulacion rapida (intervalos de 2 segundos)" -ForegroundColor White
Write-Host "2. Simulacion realista (intervalos de 3-8 minutos por split)" -ForegroundColor White
Write-Host "3. Simulacion instantanea (sin pausas)" -ForegroundColor White
$mode = Read-Host "Ingrese opcion (1, 2 o 3)"

switch ($mode) {
    "1" { 
        $delayBetweenSplits = 2
        $delayBetweenRunners = 1
        $minutesPerSplit = 0.033  # 2 segundos
    }
    "2" { 
        $delayBetweenSplits = 0
        $delayBetweenRunners = 2
        $minutesPerSplit = 5  # 5 minutos entre splits
    }
    "3" { 
        $delayBetweenSplits = 0
        $delayBetweenRunners = 0
        $minutesPerSplit = 0.05
    }
    default { 
        Write-Host "Opcion invalida. Usando modo rapido." -ForegroundColor Red
        $delayBetweenSplits = 2
        $delayBetweenRunners = 1
        $minutesPerSplit = 0.033
    }
}

Write-Host ""
Write-Host "Iniciando simulacion..." -ForegroundColor Green
Write-Host ""

$totalReadings = 0
$successfulReadings = 0

# Simular cada carrera
foreach ($race in $races) {
    Write-Host "================================================" -ForegroundColor Magenta
    Write-Host "  $($race.Name) - Splits: $($race.Splits -join ', ')" -ForegroundColor Magenta
    Write-Host "  Corredores: $($race.Runners.Count)" -ForegroundColor Magenta
    Write-Host "================================================" -ForegroundColor Magenta
    Write-Host ""
    
    # Hora de inicio base para esta carrera
    $raceStartTime = Get-Date "2025-10-15 08:00:00"
    
    # Simular cada corredor
    foreach ($chipId in $race.Runners) {
        Write-Host ">>> Corredor con Chip #$chipId iniciando carrera..." -ForegroundColor Cyan
        
        $currentTime = $raceStartTime
        
        # Pasar por cada split
        foreach ($splitId in $race.Splits) {
            # Agregar variación aleatoria de tiempo (±20%)
            $variation = Get-Random -Minimum 0.8 -Maximum 1.2
            $splitTime = $minutesPerSplit * $variation
            $currentTime = $currentTime.AddMinutes($splitTime)
            
            $totalReadings++
            if (Send-ChipReading -ChipId $chipId -SplitId $splitId -Timestamp $currentTime) {
                $successfulReadings++
            }
            
            if ($delayBetweenSplits -gt 0) {
                Start-Sleep -Seconds $delayBetweenSplits
            }
        }
        
        Write-Host ""
        
        # Pequeña pausa entre corredores
        if ($delayBetweenRunners -gt 0) {
            Start-Sleep -Seconds $delayBetweenRunners
        }
        
        # Incrementar tiempo de inicio para el siguiente corredor (salen escalonados)
        $raceStartTime = $raceStartTime.AddSeconds(30)
    }
    
    Write-Host ""
}

# Resumen final
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  RESUMEN DE SIMULACION" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Total de lecturas enviadas: $totalReadings" -ForegroundColor White
Write-Host "Lecturas exitosas: $successfulReadings" -ForegroundColor Green
Write-Host "Lecturas fallidas: $($totalReadings - $successfulReadings)" -ForegroundColor Red
Write-Host ""

if ($successfulReadings -eq $totalReadings) {
    Write-Host "Simulacion completada exitosamente!" -ForegroundColor Green
} else {
    Write-Host "Simulacion completada con errores. Revise los logs." -ForegroundColor Yellow
}

Write-Host ""
