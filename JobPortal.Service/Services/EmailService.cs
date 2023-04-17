using JobPortal.Service.AppSettings;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(Mail mailDto)
        {
            using (var msg = new MailMessage())
            {
                msg.From = new MailAddress(_mailSettings.Mail);

                if (mailDto.ToEmail != null)
                {
                    foreach (var m in mailDto.ToEmail)
                    {
                        if (!String.IsNullOrEmpty(m) || !String.IsNullOrWhiteSpace(m))
                        {
                            msg.To.Add(m);
                        }
                    }
                }

                if (mailDto.CC != null)
                {
                    foreach (var m in mailDto.CC)
                    {
                        if (!String.IsNullOrEmpty(m) || !String.IsNullOrWhiteSpace(m))
                        {
                            msg.CC.Add(m);
                        }
                    }
                }

                if (mailDto.BCC != null)
                {
                    foreach (var m in mailDto.BCC)
                    {
                        if (!String.IsNullOrEmpty(m) || !String.IsNullOrWhiteSpace(m))
                        {
                            msg.Bcc.Add(m);
                        }
                    }
                }

                msg.Subject = mailDto.Subject;
                msg.Body = mailDto.Body;

                using (var client = new SmtpClient())
                {
                    client.Host = _mailSettings.Host;
                    client.Port = _mailSettings.Port;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                    client.EnableSsl = _mailSettings.EnableSSL;
                    await client.SendMailAsync(msg);
                }
            }
        }
    }
}
