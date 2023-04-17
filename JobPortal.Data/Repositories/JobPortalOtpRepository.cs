using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalOtpRepository : JobPortalRepository<JobPortalOtp>, IJobPortalOtpRepository
    {
        public JobPortalOtpRepository(JobDbContext context) : base(context)
        {

        }
    }
}
