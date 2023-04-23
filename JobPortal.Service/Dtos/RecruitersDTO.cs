using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class RecruitersDTO
    {
        public int RecruiterId { get; set; }
        public string RecruiterName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
