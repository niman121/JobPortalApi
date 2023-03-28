using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasswordHashTool;

namespace JobPortal.Controllers
{
    [Route("account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {

        }
        [Route("sign-up")]
        [HttpGet]
        public ActionResult SignUp()
        {
            var model = new SignUpDto();
            return Ok(model);
        }

        [Route("sign-up")]
        [HttpPost]
        public ActionResult SignUp(SignUpDto dto)
        {
            if (ModelState.IsValid)
            {
                
                return Ok();
            }
            return ValidationProblem();
        }

        [Route("login")]
        [HttpPost]
        public ActionResult login(SignUpDto dto)
        {
            return Ok();
        }

        [Route("resetpassword")]
        [HttpGet]
        public ActionResult PasswordReset()
        {
            return Ok();
        }

        [Route("forgotpassword")]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return Ok();
        }
    }
}
