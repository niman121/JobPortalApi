using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class CandidatesWithJobsDto
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public List<JobDTO> jobs { get; set; }

    }
}
