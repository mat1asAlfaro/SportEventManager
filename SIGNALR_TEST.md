# 🏁 Prueba de SignalR en Tiempo Real

## ✅ Cambios Aplicados

1. **Hub fuertemente tipado** (`RaceHub : Hub<IRaceClient>`)
2. **Grupos por carrera** (cada cliente se une a `race_{raceId}`)
3. **Middleware reordenado** (CORS, antiforgery)
4. **Logging detallado** para SignalR
5. **Componente con `@rendermode InteractiveServer`**

## 🧪 Cómo Probar

### Paso 1: Iniciar la aplicación
```powershell
cd c:\Users\eliva\Documents\NET\Test\SportEventManager
dotnet run
```

### Paso 2: Abrir la página en el navegador
Navega a: **http://localhost:5221/race-live/1**

Deberías ver:
- "Conectado..." en verde
- "Estado: Conectado a grupo race_1"

### Paso 3: Enviar actualizaciones manuales

#### Opción A: Con curl (una sola actualización)
```powershell
curl -Method POST http://localhost:5221/api/racesimulation/update/1/5/3.2
```

#### Opción B: Con el simulador automático
En otra terminal PowerShell:
```powershell
.\test-signalr.ps1
```

Esto enviará actualizaciones cada 2 segundos para 4 dorsales diferentes.

### Paso 4: Verificar en el navegador
Deberías ver aparecer en la lista:
```
Carrera: 1 | Dorsal: 5 | Distancia: 3.2 km
Carrera: 1 | Dorsal: 10 | Distancia: 1.8 km
...
```

## 🔍 Diagnóstico

### Ver logs del servidor
La consola mostrará:
```
[HUB] Cliente conectado: xxx
[HUB] Conexión xxx unida a grupo race_1
[CONTROLLER] Enviando actualización a race_1: Dorsal=5, Distancia=3.2
```

### Ver logs del cliente (consola del navegador F12)
```
[RAZOR] Conexión iniciada. Estado: Connected
[RAZOR] Unido al grupo race_1
[RAZOR] Mensaje recibido: Race=1, Bib=5, Dist=3.2
```

## ❌ Problemas Comunes

### La página dice "Intentando Conexion..."
- Verifica que el servidor está corriendo
- Abre DevTools (F12) > Console y busca errores
- Revisa que la URL sea `http://localhost:5221` (no HTTPS si no está configurado)

### El servidor recibe POST pero la UI no actualiza
- Verifica en la consola del navegador si aparece `[RAZOR] Mensaje recibido`
- Si no aparece, el problema es que el cliente no está recibiendo del hub
- Revisa que el `raceId` en la URL coincida con el del POST

### Error 404 en archivos SignalR JS
- **Ya resuelto**: eliminamos las referencias JS de `App.razor`
- Blazor Server usa el cliente .NET, no necesita JS

## 🎯 Arquitectura

```
[Navegador]
    ↓ (HTTP GET /race-live/1)
[Blazor Component InteractiveServer]
    ↓ (conexión SignalR .NET)
[RaceHub] ← (grupo: race_1)
    ↑
[RaceSimulationController]
    ↑ (HTTP POST)
[Simulador / Sistema Externo]
```

## 📝 Próximas Mejoras

- [ ] Persistir actualizaciones en base de datos
- [ ] Agregar timestamp a cada actualización
- [ ] Validar que distancia no retroceda
- [ ] UI más rica (gráfico de posiciones, mapas)
- [ ] Autenticación para el POST endpoint
