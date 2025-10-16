using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SportEventManager.Components;
using SportEventManager.Core;
using SportEventManager.Data;
using SportEventManager.Data.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();

builder.Services.AddDbContext<SportEventDbContext>(Options =>
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
