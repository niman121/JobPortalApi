using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data.Repositories
{
    public class JobPortalCandidateRepository : JobPortalRepository<Candidate>, IJobPortalCandidateRepository
    {
        public JobPortalCandidateRepository(JobDbContext context) : base(context)   
        {

        }
    }
}
