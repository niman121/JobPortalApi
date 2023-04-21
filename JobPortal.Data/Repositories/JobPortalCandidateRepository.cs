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
    public class JobPortalCandidateRepository : JobPortalRepository<Candidate>, IJobPortalCandidateRepository
    {
        private readonly JobDbContext _context;

        public JobPortalCandidateRepository(JobDbContext context) : base(context)   
        {
            _context = context;
        }

        public async Task<int> AddJobs(int candidateId,List<int> jobId)
        {
            foreach (var job in jobId)
            {
                var cjob = new CandidateJob();
                cjob.CandidateId = candidateId;
                cjob.AppliedDate = DateTime.Now;
                cjob.JobId = job;
                await _context.AddAsync(cjob);
            }
            var rows = await _context.SaveChangesAsync();
            return rows;    
        }

        public async Task<List<Job>> GetJobsByCandidateId(int candidateId)
        {
            var result = new List<Job>();
            var jobIds = await _context.CandidateJob.Where(q => q.CandidateId== candidateId).
                                  OrderByDescending(o => o.AppliedDate).Select(s => s.JobId).ToListAsync();

            foreach(var id in jobIds)
            {
                var job = await _context.Jobs.FindAsync(id);
                result.Add(job);
            }

            return result;
        }
    }
}
