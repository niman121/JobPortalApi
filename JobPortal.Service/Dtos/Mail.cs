using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class Mail
    {
        public List<string> ToEmail { get; set; }
        public List<string>? BCC { get; set; }
        public List<string> CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string? Attachment { get; set; }
    }
}
