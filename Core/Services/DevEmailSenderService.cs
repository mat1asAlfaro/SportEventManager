using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SportEventManager.Core;

namespace SportEventManager.Services {
    // En desarrollo: simula envío de email escribiendo en logs
    public class DevEmailSenderService : IEmailSender {
        private readonly ILogger<DevEmailSenderService> _logger;
        public DevEmailSenderService(ILogger<DevEmailSenderService> logger) => _logger = logger;

        public Task SendAsync(string toEmail, string subject, string body) {
            _logger.LogInformation("""
========================================
DEV EMAIL (simulado)
To: {To}
Subject: {Subject}
Body:
{Body}
========================================
""", toEmail, subject, body);

            return Task.CompletedTask;
        }
    }
}