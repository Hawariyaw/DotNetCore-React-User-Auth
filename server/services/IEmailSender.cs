using System.Threading.Tasks;

namespace server.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
        Task Execute(string apiKey, string subject, string message, string email);
    }
}