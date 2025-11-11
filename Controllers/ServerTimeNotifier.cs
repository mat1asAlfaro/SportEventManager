
// using Microsoft.AspNetCore.SignalR;
// using SportEventManager.Core.Hubs;

// namespace SportEventManager.Controllers
// {
//     public class ServerTimeNotifier : BackgroundService
//     {
//         private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
//         private readonly ILogger<ServerTimeNotifier> _logger;
//         private readonly IHubContext<RaceHub, INotificationClient> _context;

//         public ServerTimeNotifier(
//             ILogger<ServerTimeNotifier> logger,
//             IHubContext<RaceHub, INotificationClient> context)
//         {
//             _logger = logger;
//             _context = context;

//             _logger.LogWarning(">>> HubContext INJECTED? " + (_context != null));
//         }

//         protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//         {
//             using var timer = new PeriodicTimer(Period);

//             while (!stoppingToken.IsCancellationRequested &&
//                 await timer.WaitForNextTickAsync(stoppingToken))
//             {
//                 _logger.LogWarning(">>> SENDING MESSAGE TO ALL CLIENTS");

//                 await _context.Clients.All.ReceiveRaceUpdate("TEST_MESSAGE");
//             }
//         }
//     }
// }
