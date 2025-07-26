using nastrafarmapi.Interfaces;
using System.Net.Mail;

namespace nastrafarmapi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<EmailService> logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<ServiceResult<bool>> SendEmailAsync(
            string to,
            string subject,
            string templateName,
            Dictionary<string, string>? variables = null,
            IEnumerable<Attachment>? attachments = null)
        {
            var resultObj = new ServiceResult<bool>();

            string body = string.Empty;
            if (!string.IsNullOrEmpty(templateName))
            {
                body = await ReplaceEmailBodyVarsAsync(templateName, variables ?? new Dictionary<string, string>());
            }

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration["EmailSettings:EmailSender"]!),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,

            };
            mailMessage.To.Add(to);

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    mailMessage.Attachments.Add(attachment);
                }
            }

            try
            {
                using var smtpClient = new SmtpClient
                {
                    Host = configuration["EmailSettings:Host"]!,
                    Port = configuration.GetValue<int>("EmailSettings:Port"),
                    EnableSsl = configuration.GetValue<bool>("EmailSettings:EnableSsl"),
                    Credentials = new System.Net.NetworkCredential
                    {
                        UserName = configuration["EmailSettings:EmailSender"]!,
                        Password = configuration["EmailSettings:EmailPassword"]!
                    }
                };

                await smtpClient.SendMailAsync(mailMessage);
                logger.LogInformation($"Email sent to {to} with subject '{subject}'");

                resultObj.Data = true;
                resultObj.Success = true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to send email to {to} with subject '{subject}'");
                resultObj.Errors.Add($"Failed to send email: {ex.Message}");
                resultObj.Success = false;
            }

            return resultObj;
        }

        private async Task<string> GetEmailTemplateAsync(string templateName)
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, "EmailTemplates", templateName);
            return await File.ReadAllTextAsync(path);
        }

        private async Task<string> ReplaceEmailBodyVarsAsync(string templateName, Dictionary<string, string> variables)
        {
            var template = await GetEmailTemplateAsync(templateName);

            foreach (var variable in variables)
            {
                template = template.Replace($"{{{variable.Key}}}", variable.Value);
            }

            return template;
        }
    }
}
