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

namespace JobPortal.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtHelper _jwtHelper;

        public AccountService(IUnitOfWork unitOfWork
                             ,IJwtHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
        }
        public Task<bool> Logout(int userId)
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

        public Task<bool> ResetPassword(ResetPasswordDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _unitOfWork.UserRepository.AnyAsync(q => q.Email == email);
        }

        public async Task<string> AuthenticateUser(SignUpDto dto)
        {
            string token = string.Empty;
            var user  = await _unitOfWork.UserRepository.GetFirstOrDefaultAsync(q => q.Email == dto.EmailAddress,true);
            var correctPassword = user.Password;
            
            var validPassword = PasswordHashManager.ValidatePassword(dto.Password, correctPassword);
            if (validPassword)
            {
                token = _jwtHelper.GenerateToken(user.Name, "Admin", user.Email);
            }
            return token;
        }
    }
}
