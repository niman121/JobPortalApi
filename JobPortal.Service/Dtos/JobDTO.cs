using JobPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class JobDTO
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<JobType> Type { get; set; }
        public DateTime PostedDate { get; set; }
        public decimal Salary { get; set; }
        public string  Location { get; set; }
    }
}
