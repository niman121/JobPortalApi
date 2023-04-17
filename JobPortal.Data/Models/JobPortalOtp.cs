using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace JobPortal.Data.Models
{
    public class JobPortalOtp
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Otp { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
