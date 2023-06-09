﻿using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(SignUpDto dto);
        Task<string> AuthenticateUserAsync(SignUpDto dto);
        Task<bool> LogoutAsync(int userId);
        Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
        Task<bool> IsEmailExistAsync(string email);
        Task<User> GetUser(HttpContext context);

    }
}
