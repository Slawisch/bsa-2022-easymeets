﻿using EasyMeets.Emailer.WebAPI.Services.Interfaces;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace EasyMeets.Emailer.WebAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;
        private readonly ILogger<EmailService> _logger;
        private readonly TransactionalEmailsApi _api;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _apiKey = configuration["Sendinblue:ApiKey"];
            _senderEmail = configuration["Sender:Email"];
            _senderName = configuration["Sender:Name"];
            _logger = logger;
            _api = new TransactionalEmailsApi();
        }

        public async Task<string> SendEmail(string recipient, string body, string subject)
        {
            Configuration.Default.ApiKey.Add("api-key", _apiKey);
            var Email = new SendSmtpEmailSender(_senderName, _senderEmail);
            var smtpEmailTo = new SendSmtpEmailTo(recipient);
            List<SendSmtpEmailTo> To = new() { smtpEmailTo};
            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, body, subject);
                CreateSmtpEmail result = await _api.SendTransacEmailAsync(sendSmtpEmail);

                return result.MessageId;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Email wasn't sent");
                return string.Empty;
            }
        }
    }
}