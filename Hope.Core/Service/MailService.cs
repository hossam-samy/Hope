using Hope.Core.Interfaces;
using Hope.Domain.Common;
using Hope.Domain.Helpers;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Hope.Core.Service
{
    public class MailService:IMailService
    {
        private readonly IDistributedCache cache;
        private readonly IStringLocalizer<MailService> localizer;
        private readonly MailSettings mailSettings;
        public MailService(IOptions<MailSettings> mailSettings, IDistributedCache cache, IStringLocalizer<MailService> localizer)
        {
            this.mailSettings = mailSettings.Value;
            this.cache = cache;
            this.localizer = localizer;
        }

        public async Task SendEmailAsync(string UserEmail)
        {
            var random=new Random();
            var key = random.Next(99999, 1000000);
            await  cache.SetStringAsync("key",key.ToString());
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(mailSettings.DisplayName,mailSettings.Email ));
            email.To.Add(new MailboxAddress("User", UserEmail));

            email.Subject = "Confirmation of Email Address";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<b>{key}</b>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, false);

                smtp.Authenticate(mailSettings.UserName,mailSettings.Password );

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        public async Task<Response> GetConfirmationNumber(string num) =>
            num == await cache.GetStringAsync("key") ? await Response.SuccessAsync(localizer["Success"]) : await Response.FailureAsync(localizer["Faild"]);
    }
}
