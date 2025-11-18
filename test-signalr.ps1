# Script de prueba para enviar actualizaciones simuladas a la carrera
Write-Host "🏁 Simulador de actualizaciones de carrera" -ForegroundColor Green
Write-Host "Presiona Ctrl+C para detener" -ForegroundColor Yellow
Write-Host ""

$raceId = 1
$baseUrl = "http://localhost:5221/api/racesimulation/update"

# Simula varios dorsales avanzando
$runners = @(
    @{Bib=1; Distance=0.0},
    @{Bib=5; Distance=0.0},
    @{Bib=10; Distance=0.0},
    @{Bib=15; Distance=0.0}
)

$iteration = 0
while ($true) {
    $iteration++
    Write-Host "`n--- Iteración $iteration ---" -ForegroundColor Cyan
    
    foreach ($runner in $runners) {
        # Incrementa la distancia aleatoriamente
        $runner.Distance += (Get-Random -Minimum 0.1 -Maximum 0.5)
        $runner.Distance = [Math]::Round($runner.Distance, 2)
        
        $url = "$baseUrl/$raceId/$($runner.Bib)/$($runner.Distance)"
        
        try {
            $response = Invoke-RestMethod -Method POST -Uri $url
            Write-Host "✓ Dorsal $($runner.Bib): $($runner.Distance) km" -ForegroundColor Green
        }
        catch {
            Write-Host "✗ Error en dorsal $($runner.Bib): $_" -ForegroundColor Red
        }
        
        Start-Sleep -Milliseconds 500
    }
    
    Start-Sleep -Seconds 2
}
