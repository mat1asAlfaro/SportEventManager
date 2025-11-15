using System.Threading.Tasks;

namespace SportEventManager.Core
{
    public interface IEmailSender
    {
        Task SendAsync(string toEmail, string subject, string body);
    }
}