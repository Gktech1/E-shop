
using Mailjet.Client.Resources;
using Mailjet.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;


namespace Ordering.Infrastructure.Repositories
{
    public class EmailService : IEmailService
    {

        private readonly IMailjetClient _client;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IMailjetClient client, ILogger<EmailService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<bool> SendEmailAsync(EmailRequest mailRequest)
        {
            try
            {
                var response = false;
                var message = new JObject
                {
                    {
                        "From", new JObject
                        {
                            {"Email", "favourblessing1@gmail.com"},
                            {"Name", "E-Shop"}
                        }
                    },
                    {
                        "To", new JArray
                        {
                            new JObject
                            {
                                {"Email", mailRequest.ToEmail},
                            }
                        }
                    },
               
                    {"Subject", mailRequest.Subject},
                    {"HtmlPart", mailRequest.Body},
                    {"CustomId", "AppGettingStartedTest"}
                };

                MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                    .Property(Send.Messages, new JArray { message });

                MailjetResponse mailjetResponse = await _client.PostAsync(request);
                if (mailjetResponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation(string.Format("Total: {0}, Count: {1}\n", mailjetResponse.GetTotal(), mailjetResponse.GetCount(), mailjetResponse.GetData()));
                   // _logger.LogInformation(mailjetResponse.GetData());
                    response = true;
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetEmailTemplate(string templateName)
        {
            var baseDir = Directory.GetCurrentDirectory();
            string folderName = "/StaticFiles/";
            var path = Path.Combine(baseDir + folderName, templateName);
            return File.ReadAllText(path);
        }

     
    }
}

/*public class MailService : IMailService
{
    private readonly IMailjetClient _client;
    private readonly ILogger<MailService> _logger;

    public MailService(IMailjetClient client, ILogger<MailService> logger)
    {
        _client = client;
        _logger = logger;
    }
    public async Task<bool> SendEmailAsync(MailRequest mailRequest)
    {
        try
        {
            var response = false;
            var message = new JObject
        {
            {
                "From", new JObject
                {
                    {"Email", "sheyeogunsanmi@gmail.com"},
                    {"Name", "ContentPrime"}
                }
            },
            {
                "To", new JArray
                {
                    new JObject
                    {
                        {"Email", mailRequest.ToEmail},
                    }
                }
            },
            {"Subject", mailRequest.Subject},
            {"HtmlPart", mailRequest.Body},
            {"CustomId", "AppGettingStartedTest"}
        };

            MailjetRequest request = new MailjetRequest { Resource = SendV31.Resource }
                .Property(Send.Messages, new JArray { message });

            MailjetResponse mailjetResponse = await _client.PostAsync(request);
            if (mailjetResponse.IsSuccessStatusCode)
            {
                _logger.Information(string.Format("Total: {0}, Count: {1}\n", mailjetResponse.GetTotal(), mailjetResponse.GetCount()));
                _logger.Information(response.GetData());
                response = true;
            }
            return response;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public string GetEmailTemplate(string templateName)
    {
        var baseDir = Directory.GetCurrentDirectory();
        string folderName = "/StaticFiles/";
        var path = Path.Combine(baseDir + folderName, templateName);
        return File.ReadAllText(path);
    }
}*/
//}

/* public EmailSettings _emailSettings { get; }
      public ILogger<EmailService> _logger { get; }

      public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
      {
          _emailSettings = emailSettings.Value;
          _logger = logger;
      }

      public async Task<bool> SendEmail(Email email)
      {
          var client = new SendGridClient(_emailSettings.ApiKey);

          var subject = email.Subject;
          var to = new EmailAddress(email.To);
          var emailBody = email.Body;

          var from = new EmailAddress
          {
              Email = _emailSettings.FromAddress,
              Name = _emailSettings.FromName
          };

          var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
          var response = await client.SendEmailAsync(sendGridMessage);

          _logger.LogInformation("Email sent.");

          if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
              return true;

          _logger.LogError("Email sending failed.");
          return false;
      }*/