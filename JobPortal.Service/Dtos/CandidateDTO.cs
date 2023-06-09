﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class CandidateDTO
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
