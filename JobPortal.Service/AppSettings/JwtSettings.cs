using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.AppSettings
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int ExpiryMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
