namespace ASAP_Task.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string mailto, string subject, string body ,IList<IFormFile> attachment= null);
    }
}
