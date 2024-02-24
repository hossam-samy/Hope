using Hope.Domain.Common;

namespace Hope.Core.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string UserEmail);
        Task<Response> GetConfirmationNumber(string num);
    }
}
