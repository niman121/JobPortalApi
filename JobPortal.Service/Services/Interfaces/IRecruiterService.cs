using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface IRecruiterService
    {
        Task<bool> AddNewJob(AddNewJobDTO job);
        Task<IEnumerable<CandidateJobDTO>> SeeCandidateList(int recruiterId,int from,int to);
        Task<CandidateJobDTO> GetCandidateBasedOnJob(int jobId);
    }
}
