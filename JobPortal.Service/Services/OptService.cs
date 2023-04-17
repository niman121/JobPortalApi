using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services
{
    public class OptService : IOtpService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public OptService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async Task GenerateAndSendOtpAsync(string emailAdress, string OtpGenerationReason)
        {
            var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(q => q.Email == emailAdress);
            if (user == null) throw new NullReferenceException("user not found");
            var Otp = await AddUserOtpAsync(user.Id);
            if (Otp == null) throw new NullReferenceException("Invalid Otp");

            var to = new List<string>()
            {
                new string(user.Email)
            };

            await SendOtpEmailAsync(Otp.Otp,to,null,null, OtpGenerationReason);
        }

        public async Task<bool> ValidateOtpAsync(int otp, string emailAddress)
        {
            var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(q => q.Email == emailAddress);
            if (user == null) throw new NullReferenceException("user not found!");

            var userOtp = await _unitOfWork.OtpRepository.GetFirstOrDefaultAsync(q => q.UserId == user.Id && q.Otp == otp && q.ExpiryDate > DateTime.Now);

            return userOtp == null ? false : true;
        }

        private async Task<JobPortalOtp> AddUserOtpAsync(int userId)
        {
            var otp = new JobPortalOtp()
            {
                UserId = userId,
                Otp = new Random().Next(1111, 9999),
                CreateDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMinutes(2)
            };
            await _unitOfWork.OtpRepository.AddAsync(otp);
            await _unitOfWork.CommitAsync();
            return otp;
        }

        private async Task SendOtpEmailAsync(int otp, List<string> to, List<string>? cc, List<string>? bcc, string OtpGenerationReason)
        {
            var mail = new Mail();
            mail.ToEmail = to;
            mail.CC = cc;
            mail.BCC = bcc;
            mail.Subject = "job Portal Otp";
            mail.Body = $"use {otp} for {OtpGenerationReason}";
            await _emailService.SendEmailAsync(mail);
        }
    }
}
