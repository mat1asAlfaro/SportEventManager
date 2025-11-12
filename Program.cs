using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SportEventManager.Components;
using SportEventManager.Core;
using SportEventManager.Core.Services;
using SportEventManager.Data;
using SportEventManager.Data.Persistence;
using SportEventManager.Models;
using SportEventManager.Controllers;
using Microsoft.AspNetCore.ResponseCompression;
using Radzen;
using SportEventManager.Hubs;
using SportEventManager.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllers();

// Servicio singleton para notificaciones en tiempo real
builder.Services.AddSingleton<RaceUpdateService>();

builder.Services.AddRadzenComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/admin";
        options.Cookie.MaxAge = TimeSpan.FromHours(1);
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<ISplitRepository, SplitRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<ITimeRecordRepository, TimeRecordRepository>();
builder.Services.AddScoped<ITimingCalculationsService, TimingCalculationsService>();

builder.Services.AddDbContextFactory<SportEventDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapControllers();

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
    ITimeRecordRepository timeRecordRepository,
    ILogger<Program> logger) =>
{
    try
    {
        if (reading.ChipId <= 0 || reading.SplitId <= 0)
        {
            logger.LogWarning("Invalid chip reading: ChipId or SplitId is invalid");
            return Results.BadRequest(new { error = "ChipId and SplitId must be greater than 0" });
        }

        var result = await timeRecordRepository.RegisterChipReadingAsync(reading);

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

// Obtener todos los tiempos de una carrera
app.MapGet("/api/timing/race/{raceId}", async (
    int raceId,
    ITimeRecordRepository timeRecordRepository) =>
{
    var records = await timeRecordRepository.GetTimeRecordsByRaceAsync(raceId);
    return Results.Ok(records);
})
.WithName("GetTimeRecordsByRace")
.WithOpenApi();

// Obtener estadísticas de una carrera
app.MapGet("/api/timing/race/{raceId}/stats", async (
    int raceId,
    ITimeRecordRepository timeRecordRepository) =>
{
    var stats = await timeRecordRepository.GetRaceStatsAsync(raceId);
    return Results.Ok(stats);
})
.WithName("GetRaceStats")
.WithOpenApi();

// Consulta en vivo por número de dorsal
app.MapGet("/api/timing/race/{raceId}/live/bib/{bibNumber}", async (
    int raceId,
    int bibNumber,
    ITimeRecordRepository timeRecordRepository,
    ILogger<Program> logger) =>
{
    try
    {
        var data = await timeRecordRepository.GetLiveParticipantDataByBibAsync(raceId, bibNumber);

        if (data == null)
        {
            logger.LogWarning($"No data found for bib {bibNumber} in race {raceId}");
            return Results.NotFound(new { error = $"No se encontró el dorsal {bibNumber} en esta carrera" });
        }

        return Results.Ok(data);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, $"Error getting live data for bib {bibNumber}");
        return Results.Problem(
            detail: ex.Message,
            statusCode: 500,
            title: "Error al obtener datos en vivo"
        );
    }
})
.WithName("GetLiveParticipantDataByBib")
.WithOpenApi();

app.Run();