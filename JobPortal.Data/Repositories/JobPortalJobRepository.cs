using JobPortal.Data.Models;
using JobPortal.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories
{
    public class JobPortalJobRepository : JobPortalRepository<Job>,IJobPortalJobRepository
    {
        private readonly JobDbContext _context;

        public JobPortalJobRepository(JobDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeActivateJob(int jobId)
        {
            var job = await GetByIdAsync(jobId);

            if (job != null)
            {
                job.IsActive = false;
                _context.Entry(job).State = EntityState.Modified;
                var jobMappings = await _context.CandidateJob.Where(q => q.JobId == jobId).ToListAsync();
                Parallel.ForEach(jobMappings, jm =>
                {
                    jm.IsActive = false;
                    _context.Entry(jm).State = EntityState.Modified;
                });
            }
        }
    }
}
