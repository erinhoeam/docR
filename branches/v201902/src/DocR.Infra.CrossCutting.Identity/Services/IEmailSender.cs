using System.Threading.Tasks;

namespace DocR.Infra.CrossCutting.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string emailTo, string emailFrom, string subject, string message, string apiKey);
    }
}
