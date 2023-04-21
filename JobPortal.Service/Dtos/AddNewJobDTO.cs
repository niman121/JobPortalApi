using JobPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Service.Dtos
{
    public class AddNewJobDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<JobType> Type { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public int NumberOfOpenings { get; set; }
        public int CreatedBy { get; set; }
    }

}
