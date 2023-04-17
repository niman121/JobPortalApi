using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface IOtpService
    {
        Task GenerateAndSendOtpAsync(string emailAddress, string OtpGenerationReason);

        Task<bool> ValidateOtpAsync(int otp, string emailAddress);
    }
}
