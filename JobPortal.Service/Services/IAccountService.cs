using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services
{
    interface IAccountService
    {
        Task<bool> RegisterUser(SignUpDto dto);
        Task<bool> AuthenticateUser(SignUpDto dto);
        Task<bool> Logout(int userId);
        Task<bool> ResetPassword(ResetPasswordDto dto);
    }
}
