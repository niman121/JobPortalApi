using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobPortal.Data.Models
{
    public class CandidateJob
    {
        [Key, Column(Order =0), ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        
        [Key,Column(Order =1) ,ForeignKey("Job")]
        public int JobId { get; set; }
        public Job Job { get; set; }
        public DateTime AppliedDate { get; set; }

        public bool IsActive { get; set; }
    }
}
