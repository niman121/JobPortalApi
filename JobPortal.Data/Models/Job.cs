using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace JobPortal.Data.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public int NumberOfOpenings { get; set; }
        public Recruiter Recruiter { get; set; }
        public List<Candidate> Candidates { get; set; }
        public List<JobType> JobType { get; set; }
    }
}
