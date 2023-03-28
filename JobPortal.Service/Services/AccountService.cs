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

namespace JobPortal.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly JobDbContext _context;

        public AccountService(JobDbContext jobDbContext)
        {
            _context = jobDbContext;
        }
        public Task<bool> AuthenticateUser(SignUpDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(SignUpDto dto)
        {
            int rowSaved = 0;
            var exists = await IsEmailExistAsync(dto.EmailAddress);
            if (!exists)
            {
                var hashedPassword = PasswordHashManager.CreateHash(dto.Password);
                var user = new User();
                user.CreatedDate = DateTime.Now;
                user.Email = dto.EmailAddress;
                user.ModifiedDate = null;
                user.Name = dto.EmailAddress;
                user.Password = hashedPassword;
                await _context.Users.AddAsync(user);
                rowSaved = await _context.SaveChangesAsync();
            }
            else
            {
                return false;
            }
            if (rowSaved > 0) return true;
            return false;
        }

        public Task<bool> ResetPassword(ResetPasswordDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(q => q.Email == email);
        }
    }
}
