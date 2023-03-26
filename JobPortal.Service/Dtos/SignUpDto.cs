using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class SignUpDto
    {
        [DataType(DataType.EmailAddress,ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage ="Email is Required")]
        [StringLength(256,ErrorMessage ="email size is greater")]
        public string EmailAddress { get; set; }

        [DataType(DataType.Password,ErrorMessage ="Invalid Password")]
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
            ErrorMessage = "password must have 8 characters, Atleast have One Capital Letter,One Number and one special character")]
        public string Password { get; set; }
    }
}
