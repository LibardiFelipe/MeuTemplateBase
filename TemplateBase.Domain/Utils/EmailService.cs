using System.Net;
using System.Net.Mail;
using TemplateBase.Domain.Classes;

namespace TemplateBase.Domain.Utils
{
    public static class EmailService
    {
        public static bool Send(EmailConfig config, Email email)
        {
			try
			{
                var smtpClient = new SmtpClient(config.Address)
                {
                    Port = config.Port,
                    Credentials = new NetworkCredential(config.User, config.Password),
                    EnableSsl = config.EnableSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(config.User),
                    Subject = email.Subject,
                    Body = email.Body,
                    IsBodyHtml = true,
                };

                foreach (var item in email.Addressees)
                    mailMessage.To.Add(item);

                smtpClient.Send(mailMessage);
                return true;
            }
			catch
			{
                return false;
			}
        }
    }
}
