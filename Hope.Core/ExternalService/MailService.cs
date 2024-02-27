using Hope.Core.Interfaces;
using Hope.Core.Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hope.Core.Service
{
    public class MailService:IMailService
    {
        private readonly IDistributedCache cache;
        private readonly IStringLocalizer<MailService> localizer;
        private readonly IConfiguration configuration;

        public MailService(IDistributedCache cache, IStringLocalizer<MailService> localizer, IConfiguration configuration)
        {
            this.cache = cache;
            this.localizer = localizer;
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string UserEmail)
        {
            var random=new Random();
            var key = random.Next(99999, 1000000);
            await  cache.SetStringAsync("key",key.ToString());
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(configuration["MailSettings:DisplayName"], configuration["MailSettings:Email"]));
            email.To.Add(new MailboxAddress("User", UserEmail));

            email.Subject = "Confirmation of Email Address";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<b>{key}</b>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(configuration["MailSettings:Host"], int.Parse(configuration["MailSettings:Port"]!), false);

                smtp.Authenticate(configuration["MailSettings:UserName"], configuration["MailSettings:Password"]);

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
        public async Task<Response> GetConfirmationNumber(string num) =>
            num == await cache.GetStringAsync("key") ? await Response.SuccessAsync(localizer["Success"]) : await Response.FailureAsync(localizer["Faild"]);
    }
}
