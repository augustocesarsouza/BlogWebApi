using System.Net;
using System.Net.Mail;

namespace BlogWeb.Services
{
    public class EmailService
    {
        public bool Send(
            string toName,
            string toEmail,
            string subject,
            string body,
            string fromName = "Equipe augusto.io",
            string fromEmail = "augustocesarsantana90@gmail.com")
        {
            var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port);

            smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName); // Quem Esta enviando
            mail.To.Add(new MailAddress(toEmail, toName)); // Para quem está recebendo é uma lista pode ter mais
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mail);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
    }
}
