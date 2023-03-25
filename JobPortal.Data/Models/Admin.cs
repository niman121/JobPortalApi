using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }

    }
}
