using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class ResetPasswordDto
    {
        public int UserId { get; set; }

        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "password must have 8 characters, Atleast have One Capital Letter,One Number and one special character")]
        public string Password { get; set; }
    }
}
