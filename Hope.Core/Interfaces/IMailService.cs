using Hope.Core.Common;

namespace Hope.Core.Interfaces
{
    public interface IMailService
    {
        Task<Response> SendEmailAsync(string UserEmail);

        Task<Response> SendEmailForChangePasswordAsync(string UserEmail);
        Task<Response> GetConfirmationNumber(string UserEmail, string num);
    }
}
