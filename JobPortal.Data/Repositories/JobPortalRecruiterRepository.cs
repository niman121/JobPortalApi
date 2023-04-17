using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalRecruiterRepository : JobPortalRepository<Recruiter>, IJobPortalRecruiterRepository
    {
        public JobPortalRecruiterRepository(JobDbContext context) : base(context)
        {

        }
    }
}
