using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalUserRepository : JobPortalRepository<User>, IJobPortalUserRepository
    {
        public JobPortalUserRepository(JobDbContext context) : base(context)
        {

        }
    }
}
