using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.AppSettings
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

    }
}
