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
        Task<List<CandidatesWithJobsDto>> GetAllCandidateWithAllJobs(bool? onlyActiveCandidate);
        Task<CandidatesWithJobsDto> GetCandidateJobs(int candidateId);
        Task<CandidateJobDTO> GetCandidatesByJob(int jobId);
        Task<bool> deActivateJob(int jobId);
        Task<bool> deActivateCandidate(int candidateId);
        Task<bool> deActivateRecruiter(int recruiterId);
        Task<bool> AddNewRecruiter(SignUpDto dTO);
        Task<bool> UpdateRecruiter(RecruitersDTO rDto);
        Task<byte[]> ExportCandidateWithAppliedJobs();
        byte[] ExportRecruiters();
        Task<byte[]> ExportCandidates();
    }
}
