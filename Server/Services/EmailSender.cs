using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace helping_hand.Server.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;
        private SendGridClient _sendGridClient;

        public EmailAddress From { get; set; }
        public string Subject { get; set; }
        public EmailAddress To { get; set; }
        public string PlainText { get; set; }
        public string HtmlContent { get; set; }

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeSendGridClient();
        }

        private void InitializeSendGridClient()
        {
            var apiKey = _configuration["SENDGRID_KEY"];

            if (apiKey is null)
            {
                apiKey = Environment.GetEnvironmentVariable("SENDGRID_KEY", EnvironmentVariableTarget.Machine);
            }

            if (apiKey is null) return;

            _sendGridClient = new SendGridClient(apiKey);
            From = new EmailAddress("helpinghands.auth@gmail.com", "Helping Hands");
        }

        public async Task SendEmailConfirmation(string url, string to, string toName = null)
        {
            if (_sendGridClient is null) return;

            Subject = "Email Confirmation";
            To = new EmailAddress(to, toName);
            PlainText = $"Confirm Url: {url}";
            HtmlContent = $"<a target=\"_blank\" href=\"{url}\"><strong>Confirm Email</strong></a>";
            var msg = MailHelper.CreateSingleEmail(From, To, Subject, PlainText, HtmlContent);
            var response = await _sendGridClient.SendEmailAsync(msg);
            Console.WriteLine(response.StatusCode);
        }
    }
}
