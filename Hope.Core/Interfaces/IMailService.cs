namespace Hope.Core.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string mailto, string subject,string body);
    }
}
