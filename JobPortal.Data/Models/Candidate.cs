using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JobPortal.Data.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public List<Job> Jobs { get; set; }
        public bool IsActive { get; set; }

    }
}
