using nastrafarmapi.Services;
using System.Net.Mail;

namespace nastrafarmapi.Interfaces
{
    public interface IEmailService
    {
        Task<ServiceResult<bool>> SendEmailAsync(string to, string subject, string templateName, Dictionary<string, string>? variables = null, IEnumerable<Attachment>? attachments = null);
    }
}