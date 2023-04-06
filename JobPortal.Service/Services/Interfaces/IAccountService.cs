using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(SignUpDto dto);
        Task<bool> ValidateUser(SignUpDto dto);
        Task<bool> Logout(int userId);
        Task<bool> ResetPassword(ResetPasswordDto dto);
        Task<bool> IsEmailExistAsync(string email);

    }
}
