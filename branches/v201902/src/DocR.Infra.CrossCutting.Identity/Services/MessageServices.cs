using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DocR.Infra.CrossCutting.Identity.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string emailTo, string emailFrom, string subject, string message, string apiKey)
        {
            var minhaMensagemEmail = new SendGridMessage();

            minhaMensagemEmail.AddTo(emailTo);
            minhaMensagemEmail.From = new EmailAddress(emailFrom);
            minhaMensagemEmail.Subject = subject;
            minhaMensagemEmail.HtmlContent = message;

            // Cria um transporte web para enviar email
            var transporteWeb = new SendGridClient(apiKey);
            // Enviar email
            var r = transporteWeb.SendEmailAsync(minhaMensagemEmail);

            return r;
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
