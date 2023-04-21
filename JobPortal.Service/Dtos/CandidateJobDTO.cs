using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class CandidateJobDTO
    {
        public List<CandidatesOfJobDto> Candidates { get; set; }
        public int JobId { get; set; }
        public string JobName { get; set; }
    }

    public class CandidatesOfJobDto
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
    }
}
