using JobPortal.Service.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {

        }

        public ActionResult login(SignUpDto dto)
        {
            return Ok();
        }
        public ActionResult SignUp(SignUpDto dto)
        {
            return Ok();
        }

        public ActionResult PasswordReset()
        {
            return Ok();
        }

        public ActionResult ForgotPassword()
        {
            return Ok();
        }
    }
}
