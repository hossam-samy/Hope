using Hope.Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using Hope.Domain.Helpers;

namespace Hope.Core.Service
{
    public class MailService:IMailService
    {
        private readonly MailSettings mailSettings; 
        public MailService(IOptions<MailSettings> mailSettings) {
         this.mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string mailto, string subject, string body)
        {
            var email = new MimeMessage { 
               Sender=MailboxAddress.Parse(mailSettings.Email),
               Subject=subject
   
            };
            email.To.Add(MailboxAddress.Parse("lafayette60@ethereal.email"));
            var builder = new BodyBuilder();
            {

            //if(files != null) {
            //    byte[] filebytes;
            //    foreach (var file in files)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using var ms = new MemoryStream();
            //            file.CopyTo(ms);
            //            filebytes = ms.ToArray();
            //            builder.Attachments.Add(file.FileName, filebytes, ContentType.Parse(file.ContentType));

            //        }
            //    }

            //}
            }
            builder.HtmlBody = body;
            email.Body=builder.ToMessageBody();
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, "lafayette60@ethereal.email"));

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", mailSettings.Port,SecureSocketOptions.StartTls);

            smtp.Authenticate("lafayette60@ethereal.email", "sxFdnv2wdPGSmVk6MK");

            await smtp.SendAsync(email);
           
            
            smtp.Disconnect(true);

        }
    }
}
