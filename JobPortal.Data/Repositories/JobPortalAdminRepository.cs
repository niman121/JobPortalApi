using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalAdminRepository : JobPortalRepository<Admin>,IJobPortalAdminRepository
    {
        public JobPortalAdminRepository(JobDbContext context) : base(context)
        {

        }
    }
}
