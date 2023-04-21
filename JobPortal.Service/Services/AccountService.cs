using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordHashTool;
using JobPortal.Service.Services.Interfaces;
using JobPortal.Data.Repositories.Interfaces;
using JobPortal.ApiHelper;
using Microsoft.AspNetCore.Http;

namespace JobPortal.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHelper _jwtHelper;
        private readonly IOtpService _otpService;

        public AccountService(IUnitOfWork unitOfWork
                             ,IJwtHelper jwtHelper
                             ,IOtpService otpService)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
            _otpService = otpService;
        }
        public Task<bool> LogoutAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUserAsync(SignUpDto dto)
        {
            var hashedPassword = PasswordHashManager.CreateHash(dto.Password);
            var user = new User();

            user.CreatedDate = DateTime.Now;
            user.Email = dto.EmailAddress;
            user.ModifiedDate = null;
            user.Name = dto.EmailAddress;
            user.Password = hashedPassword;

            await _unitOfWork.UserRepository.AddAsync(user);
            var rowSaved = await _unitOfWork.CommitAsync();
            if (rowSaved > 0) return true;
            return false;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            bool status = false;
            var validOtp = await _otpService.ValidateOtpAsync(dto.Otp, dto.Email);
            if (validOtp)
            {
                var newPassword = PasswordHashManager.CreateHash(dto.Password);
                var user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(q => q.Email == dto.Email);
                if (user != null)
                {
                    user.Password = newPassword;
                    _unitOfWork.UserRepository.Update(user);
                    var rowSaved = await _unitOfWork.CommitAsync();
                    if(rowSaved > 0) 
                        status = true;
                }
            }
            return status;
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _unitOfWork.UserRepository.AnyAsync(q => q.Email == email);
        }

        public async Task<string> AuthenticateUserAsync(SignUpDto dto)
        {
            string token = string.Empty;
            var user  = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(q => q.Email == dto.EmailAddress,true);
            var correctPassword = user.Password;
            var roles = user.Roles.ToList().Select(q => q.Name).ToArray();
            var validPassword = PasswordHashManager.ValidatePassword(dto.Password, correctPassword);
            if (validPassword)
            {
                token = _jwtHelper.GenerateToken(user.Name, roles, user.Email);
            }
            return token;
        }

        public async Task<User> GetUser(HttpContext context)
        {
            var user = new User();
            var userName = context.User.Identity.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                user = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(q => q.Email == userName, true);
            }
            return user;
        }
    }
}
