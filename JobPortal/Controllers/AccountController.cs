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
using JobPortal.ApiHelper;
using System.CodeDom.Compiler;

namespace JobPortal.Controllers
{
    [System.Web.Http.RoutePrefix("account")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IOtpService _otpService;

        public AccountController(IAccountService accountService, IOtpService otpService)
        {
            _accountService = accountService;
            _otpService = otpService;
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
        public async Task<ServiceResult<string>> login(SignUpDto dto)
        {
            var result = new ServiceResult<string>();
            var authenticUserToken = await _accountService.AuthenticateUserAsync(dto);

            if (String.IsNullOrEmpty(authenticUserToken))
            {
                result.SetFailure("Invalid UserName or Password");
                result.Data = authenticUserToken;
            }
            else
                result.Data = authenticUserToken;

            return result;
        }

        [Route("resetpassword")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ServiceResult> PasswordReset(ResetPasswordDto dto)
        {
            var result = new ServiceResult();
            var passwordChanged = await _accountService.ResetPasswordAsync(dto);
            if (passwordChanged)
            {
                result.SetSuccess();
                result.Message = "password changed successfully!!";
            }
            else
                result.SetFailure("password change unsuccessful.");
            return result;
        }

        [Route("forgotpassword")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ServiceResult> ForgotPassword(string emailAddress)
        {
            var result = new ServiceResult();
            var emailExists = await _accountService.IsEmailExistAsync(emailAddress);
            if (emailExists)
            {
                await _otpService.GenerateAndSendOtpAsync(emailAddress, "forgot password");
                result.SetSuccess();
                result.Message = "otp sent to email";
            }
            else
                result.SetFailure("Email does not exists!");
            return result;
        }
    }
}
