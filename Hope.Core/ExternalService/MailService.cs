using Hope.Core.Interfaces;
using Hope.Core.Common;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using MimeKit;
using Microsoft.Extensions.Configuration;
using Hope.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Hope.Core.Service
{
    public class MailService:IMailService
    {
        private readonly IDistributedCache cache;
        private readonly IStringLocalizer<MailService> localizer;
        private readonly IConfiguration configuration;
        private readonly IUnitofWork work;

        public MailService(IDistributedCache cache, IStringLocalizer<MailService> localizer, IConfiguration configuration, IUnitofWork work)
        {
            this.cache = cache;
            this.localizer = localizer;
            this.configuration = configuration;
            this.work = work;
        }

        public async Task<Response> SendEmailAsync(string UserEmail)
        {

            if (UserEmail == null) 
                return await Response.FailureAsync(localizer["EmailRequired"].Value);

            var random=new Random();
            
            var key = random.Next(99999, 1000000);
            
            await  cache.SetStringAsync($"{UserEmail}",key.ToString());
            
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(configuration["MailSettings:DisplayName"], configuration["MailSettings:Email"]));
           
            email.To.Add(new MailboxAddress("User", UserEmail));

            email.Subject = "Confirmation of Email Address";
            
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text  = $"<b>{key}هو رمز التحقق الخاص بـ Hope. </b>"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(configuration["MailSettings:Host"], int.Parse(configuration["MailSettings:Port"]!), false);

                smtp.Authenticate(configuration["MailSettings:UserName"], configuration["MailSettings:Password"]);

                smtp.Send(email);
                smtp.Disconnect(true);
            }
                return await Response.SuccessAsync(localizer["Success"].Value);  

        }
        public async Task<Response> GetConfirmationNumber( string UserEmail,string num)
        {
            if (cache.GetString(UserEmail) != num) return await Response.FailureAsync(localizer["Faild"].Value);
            else  return await Response.SuccessAsync(localizer["Success"].Value);


        }
            //=>num == await cache.GetStringAsync(num) ?  : ;

       
        public async Task<Response> SendEmailForChangePasswordAsync(string UserEmail)
        {
            if (work.Repository<User>().Get(i => i.Email == UserEmail).Result.FirstOrDefault() == null)
                return await Response.FailureAsync(localizer["EmailInvalid"].Value);


            return await SendEmailAsync(UserEmail);
            
        }
    }
}
