
using ASAP_Task.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;


namespace ASAP_Task.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string mailto, string subject, string body, IList<IFormFile> attachment = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(emailSettings.Email),  
                Subject = subject,  
            };
            email.To.Add(MailboxAddress.Parse(mailto));
            var builder = new BodyBuilder();
            if(attachment != null) 
            {
                byte[] filebytes;
                foreach (var file in attachment)
                {
                   if(file.Length>0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);
                        filebytes = ms.ToArray();
                        builder.Attachments.Add(file.FileName, filebytes , ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(emailSettings.Displayname, emailSettings.Email));
            using var smtp = new SmtpClient();
            smtp.Connect(emailSettings.Host, emailSettings.Port , SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(emailSettings.Email , emailSettings.Passowrd);
            await smtp.SendAsync(email);
            smtp.Disconnect(true); 
        }
    }
}
