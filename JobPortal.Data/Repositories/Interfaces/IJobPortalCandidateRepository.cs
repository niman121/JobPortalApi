using JobPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories.Interfaces
{
    public interface IJobPortalCandidateRepository : IJobPortalRepository<Candidate>
    {
        Task<int> AddJobs(int candidateId, List<int> jobId);
        Task<List<Job>> GetJobsByCandidateId(int candidateId);
        Task<List<Candidate>> GetCandidateBasedOnJobId(int jobId);
        Task<List<Candidate>> GetAllCandidates(bool? onlyActiveCandidates, int skip, int take);
        Task DeActivateCandidate(int candidateId);
    }
}
