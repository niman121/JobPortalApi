using JobPortal.Service;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using JobPortal.Utility;
using JobPortal.Utility.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [System.Web.Http.RoutePrefix("Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("GetAllCandidates")]
        public async Task<ServiceResult<List<CandidateDTO>>> GetAllCandidates()
        {
            var result = new ServiceResult<List<CandidateDTO>>();
            var candidates = new List<CandidateDTO>();

            var activeOnlyCandidates = true;
            candidates = await _adminService.GetAllCandidates(activeOnlyCandidates,0,100);

            if (candidates.Count > 0)
                result.SetSuccess(candidates);
            else
                result.SetFailure("could not load candidates");

            return result;
        }

        [HttpGet]
        [Route("GetAllRecruiters")]
        public async Task<ServiceResult<List<RecruitersDTO>>> GetAllRecruiters()
        {
            var result = new ServiceResult<List<RecruitersDTO>>();
            var recruiters = new List<RecruitersDTO>();

            var activeOnlyRecruiters = true;
            recruiters = await _adminService.GetAllRecruiters(activeOnlyRecruiters);
            if (recruiters.Count > 0)
                result.SetSuccess(recruiters);
            else
                result.SetFailure("could not load recruiters");
            return result;
        }

        [HttpGet]
        [Route("GetAllJobs")]
        public async Task<ServiceResult<List<JobDTO>>> GetAllJobs()
        {
            var result = new ServiceResult<List<JobDTO>>();
            var activeOnlyJobs = true;
            
            result.Data = await _adminService.GetAllJobs(activeOnlyJobs);
            
            if (result.Data.Count > 0)
                result.SetSuccess();
            else
                result.SetFailure("could not load jobs");
            
            return result;
        }

        [HttpGet]
        [Route("GetAllCandidateWithAllJobs")]
        public async Task<ServiceResult<CandidateJobDTO>> GetAllCandidateWithAllJobs()
        {
            var jobs = new List<CandidateJobDTO>();
            return jobs;
        }

        //[HttpGet]
        //[Route("GetAllCandidateJobs")]
        //public async Task<ServiceResult<CandidateAllJobDTO>> GetCandidateWithAllJobs(int candidateId)
        //{
        //    var jobs = new List<CandidateAllJobDTO>();
        //    return jobs;
        //}

        //[HttpGet]
        //[Route("GetAllJobCandidates")]
        //public async Task<ServiceResult<JobAllCandidateDTO>> GetJobWithAllCandidates(int jobId)
        //{
        //    var jobs = new List<JobAllCandidateDTO>();
        //    return jobs;
        //}

        //[HttpPost]
        //[Route("RemoveJob")]
        //[ValidateModel]
        //public async Task<ServiceResult> RemoveJob(int jobId)
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpPost]
        //[Route("RemoveCandidate")]
        //[ValidateModel]
        //public async Task<ServiceResult> RemoveCandidate(int candidateId)
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpPost]
        //[Route("RemoveRecruiter")]
        //[ValidateModel]
        //public async Task<ServiceResult> RemoveRecruiter(int recruiterId)
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpPost]
        //[Route("AddRecruiter")]
        //[ValidateModel]
        //public async Task<ServiceResult> AddNewRecruiter(RecruitersDTO recruiter)
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpPut]
        //[Route("UpdateRecruiter")]
        //[ValidateModel]
        //public async Task<ServiceResult> UpdateRecruiter(RecruitersDTO recruiter)
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpGet]
        //[Route("ExportCandidateList")]
        //public async Task<ServiceResult> ExportAllCandidates()
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpGet]
        //[Route("ExportRcruiterList")]
        //public async Task<ServiceResult> ExportAllRecruiters()
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}

        //[HttpGet]
        //[Route("ExportCandidateWithAppliedJobsList")]
        //public async Task<ServiceResult> ExportCandidatesWithAppliedJobs()
        //{
        //    var result = new ServiceResult();
        //    return result;
        //}
    }
}
