using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using server.Helpers;

namespace server.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IAppSettings _appSettings;

        public EmailSender(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var sendGridKey = _appSettings.SendGridKey;
            return Execute(sendGridKey, subject, htmlMessage, email);
        }

        public Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("youremail@domain.com", "Welcome"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}