using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using JobPortal.Service;
using Microsoft.AspNetCore.Authorization;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using AllowAnonymousAttribute = Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute;
using JobPortal.Utility;
using Microsoft.AspNetCore.Mvc;
using JobPortal.Utility.Exceptions;
using JobPortal.Service.Services.Interfaces;
using PasswordHashTool;

namespace JobPortal.Controllers
{
    [System.Web.Http.RoutePrefix("account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("sign-up")]
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<ServiceResult> SignUp(SignUpDto dto)
        {
            var result = new ServiceResult();
            var emailExists = await _accountService.IsEmailExistAsync(dto.EmailAddress);
            if (!emailExists)
            {
                var isSignedUp = await _accountService.RegisterUserAsync(dto);
                if (isSignedUp)
                    result.SetSuccess();
                else
                    result.SetFailure("User Registeration Failed");
            }
            else
                result.SetFailure("Email Already Exists");
            return result;
        }

        [Route("login")]
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public ServiceResult login(SignUpDto dto)
        {
            var result = new ServiceResult();
            return result;
        }

        [Route("resetpassword")]
        [HttpGet]
        [AllowAnonymous]
        public ServiceResult PasswordReset()
        {
            var result = new ServiceResult();
            return result;
        }

        [Route("forgotpassword")]
        [HttpGet]
        [AllowAnonymous]
        public ServiceResult ForgotPassword()
        {
            var result = new ServiceResult();
            return result;
        }
    }
}
