using Microsoft.EntityFrameworkCore;
using SportEventManager.Components;
using SportEventManager.Core;
using SportEventManager.Data;
using SportEventManager.Data.Persistence;
using SportEventManager.Services;
using SportEventManager.Hubs;
using SportEventManager.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<ISplitRepository, SplitRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddDbContextFactory<SportEventDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar repositorio de TimeRecord
builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();

// Registrar servicios de timing
builder.Services.AddScoped<ITimingCalculationsService, TimingCalculationsService>();
builder.Services.AddScoped<ITimingService, TimingService>();

// Agregar SignalR
builder.Services.AddSignalR();

// Agregar OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Mapear OpenAPI
app.MapOpenApi();

// Mapear el Hub de SignalR
app.MapHub<TimingHub>("/timingHub");

// ========================================
// MINIMAL API para registro de tiempos RFID
// ========================================

// Endpoint principal: Registrar lectura de chip
app.MapPost("/api/timing/register", async (
    ChipReadingDTO reading,
    ITimingService timingService,
    ILogger<Program> logger) =>
{
    try
    {
        if (reading.ChipId <= 0 || reading.SplitId <= 0)
        {
            logger.LogWarning("Invalid chip reading: ChipId or SplitId is invalid");
            return Results.BadRequest(new { error = "ChipId and SplitId must be greater than 0" });
        }

        var result = await timingService.RegisterChipReadingAsync(reading);
        
        if (result == null)
        {
            logger.LogWarning($"Failed to register chip reading: ChipId {reading.ChipId}, SplitId {reading.SplitId}");
            return Results.BadRequest(new { error = "Failed to register time record. Split may not exist or record already exists." });
        }

        logger.LogInformation($"Successfully registered chip reading: {result.TimeRecordId}");
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error processing chip reading");
        return Results.Problem(
            detail: ex.Message,
            statusCode: 500,
            title: "Internal Server Error"
        );
    }
})
.WithName("RegisterChipReading")
.WithOpenApi();

// Endpoint alternativo simple (para curl rápido)
app.MapPost("/api/timing/chip/{chipId}/split/{splitId}", async (
    int chipId,
    int splitId,
    ITimingService timingService) =>
{
    var reading = new ChipReadingDTO
    {
        ChipId = chipId,
        SplitId = splitId,
        Timestamp = DateTime.UtcNow
    };

    var result = await timingService.RegisterChipReadingAsync(reading);
    return result != null ? Results.Ok(result) : Results.BadRequest("Failed to register");
})
.WithName("RegisterChipReadingSimple")
.WithOpenApi();

// Obtener todos los tiempos de una carrera
app.MapGet("/api/timing/race/{raceId}", async (
    int raceId,
    ITimingService timingService) =>
{
    var records = await timingService.GetTimeRecordsByRaceAsync(raceId);
    return Results.Ok(records);
})
.WithName("GetTimeRecordsByRace")
.WithOpenApi();

// Obtener estadísticas de una carrera
app.MapGet("/api/timing/race/{raceId}/stats", async (
    int raceId,
    ITimingService timingService) =>
{
    var stats = await timingService.GetRaceStatsAsync(raceId);
    return Results.Ok(stats);
})
.WithName("GetRaceStats")
.WithOpenApi();

app.Run();