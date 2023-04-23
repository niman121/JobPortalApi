using JobPortal.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<CandidateDTO>> GetAllCandidates(bool? onlyActiveCandidates, int skip, int take);
        Task<List<RecruitersDTO>> GetAllRecruiters(bool? onlyActiveRecruiters);
        Task<List<JobDTO>> GetAllJobs(bool? onlyActiveJobs);
    }
}
