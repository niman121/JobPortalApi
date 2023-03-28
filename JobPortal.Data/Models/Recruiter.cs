using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobPortal.Data.Models
{
    public class Recruiter
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }

    }
}
