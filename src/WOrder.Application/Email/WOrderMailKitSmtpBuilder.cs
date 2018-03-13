using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Abp.Extensions;
using Abp.MailKit;
using Abp.Net.Mail.Smtp;
using Castle.Core.Logging;
using MailKit.Net.Smtp;

namespace WOrder.Email
{
    public class WOrderMailKitSmtpBuilder: DefaultMailKitSmtpBuilder
    {
        private ISmtpEmailSenderConfiguration _smtpEmailSenderConfiguration;
        private ILogger _logger;
        public WOrderMailKitSmtpBuilder(ISmtpEmailSenderConfiguration smtpEmailSenderConfiguration, ILogger logger) :base(smtpEmailSenderConfiguration)
        {
            _smtpEmailSenderConfiguration = smtpEmailSenderConfiguration;
            _logger = logger;
        }

        protected override void ConfigureClient(MailKit.Net.Smtp.SmtpClient client)
        {
            client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;

            //_logger.Info($"邮箱地址:{client.}")

            base.ConfigureClient(client);
        }

      
    }
}
