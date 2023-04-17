using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mail mailDto);
    }
}
