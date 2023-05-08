using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface ICandidateService
    {
        IEnumerable<JobDTO> GetAllJobs(int skip, int take);
        Task<bool> ApplyToJobs(int candidateId, int[] applicationIds);
        Task<List<JobDTO>> AppliedJobs(int candidateId);
    }
}
