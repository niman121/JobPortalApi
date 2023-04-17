using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class ApplyJobDTO
    {
        public int CandidateId { get; set; }
        public List<int> JobIds { get; set; }
    }
}
