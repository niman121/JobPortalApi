using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalJobRepository : JobPortalRepository<Job>,IJobPortalJobRepository
    {
        public JobPortalJobRepository(JobDbContext context) : base(context)
        {

        }
    }
}
