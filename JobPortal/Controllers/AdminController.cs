using JobPortal.Data.Models;
using JobPortal.Service;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services;
using JobPortal.Service.Services.Interfaces;
using JobPortal.Utility;
using JobPortal.Utility.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [System.Web.Http.RoutePrefix("Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;

        public AdminController(IAdminService adminService, IAccountService accountService)
        {
            _adminService = adminService;
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAllCandidates")]
        public async Task<ServiceResult<List<CandidateDTO>>> GetAllCandidates()
        {
            var result = new ServiceResult<List<CandidateDTO>>();
            var candidates = new List<CandidateDTO>();

            var activeOnlyCandidates = true;
            candidates = await _adminService.GetAllCandidates(activeOnlyCandidates, 0, 100);

            if (candidates.Count > 0)
                result.SetSuccess(candidates);
            else
                result.SetFailure("could not load candidates.");

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
                result.SetFailure("could not load recruiters.");
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
                result.SetFailure("could not load jobs.");

            return result;
        }

        [HttpGet]
        [Route("GetAllCandidateWithAllJobs")]
        public async Task<ServiceResult<List<CandidatesWithJobsDto>>> GetAllCandidateWithAllJobs()
        {
            var result = new ServiceResult<List<CandidatesWithJobsDto>>();
            bool onlyActiveCandidate = true;
            var candidatesWithJobs = await _adminService.GetAllCandidateWithAllJobs(onlyActiveCandidate);

            if (candidatesWithJobs.Count > 0)
            {
                result.SetSuccess();
                result.Data = candidatesWithJobs;
            }
            else
            {
                result.SetFailure("could not load candidate with jobs list.");
            }
            return result;
        }

        [HttpGet]
        [Route("GetAllCandidateJobs")]
        public async Task<ServiceResult<CandidatesWithJobsDto>> GetCandidateWithAllJobs(int candidateId)
        {
            var result = new ServiceResult<CandidatesWithJobsDto>();
            var candidateJobs = await _adminService.GetCandidateJobs(candidateId);

            if (candidateJobs != null)
            {
                result.SetSuccess();
                result.Data = candidateJobs;
            }
            else
            {
                result.SetFailure("could not load candidate's jobs.");
            }
            return result;
        }

        [HttpGet]
        [Route("GetAllJobCandidates")]
        public async Task<ServiceResult<CandidateJobDTO>> GetJobWithAllCandidates(int jobId)
        {
            var result = new ServiceResult<CandidateJobDTO>();

            var candidateByjob = await _adminService.GetCandidatesByJob(jobId);

            if (candidateByjob != null)
                result.SetSuccess(candidateByjob);
            else
                result.SetFailure("could not load candidates for job.");

            return result;
        }

        [HttpPost]
        [Route("RemoveJob")]
        public async Task<ServiceResult> RemoveJob(int jobId)
        {
            var result = new ServiceResult();
            var deactivated = await _adminService.deActivateJob(jobId);

            if (deactivated)
                result.SetSuccess();
            else
                result.SetFailure("could not delete job.");

            return result;
        }

        [HttpPost]
        [Route("RemoveCandidate")]
        public async Task<ServiceResult> RemoveCandidate(int candidateId)
        {
            var result = new ServiceResult();
            var deactivated = await _adminService.deActivateCandidate(candidateId);

            if (deactivated)
                result.SetSuccess();
            else
                result.SetFailure("could not delete candidate.");

            return result;
        }

        [HttpPost]
        [Route("RemoveRecruiter")]
        [ValidateModel]
        public async Task<ServiceResult> RemoveRecruiter(int recruiterId)
        {
            var result = new ServiceResult();
            var deactivated = await _adminService.deActivateRecruiter(recruiterId);

            if (deactivated)
                result.SetSuccess();
            else
                result.SetFailure("could not delete candidate.");

            return result;
        }

        [HttpPost]
        [Route("AddRecruiter")]
        [ValidateModel]
        public async Task<ServiceResult> AddNewRecruiter(SignUpDto recruiter)
        {
            var result = new ServiceResult();
            var exists = await _accountService.IsEmailExistAsync(recruiter.EmailAddress);
            if (exists)
            {
                var added = await _adminService.AddNewRecruiter(recruiter);
                if (added)
                    result.SetSuccess();
                else
                    result.SetFailure("could not add recruiter.");
            }
            else
            {
                result.SetFailure("email already exists");
            }
            return result;
        }

        [HttpPut]
        [Route("updaterecruiter")]
        [ValidateModel]
        public async Task<ServiceResult> UpdateRecruiter(RecruitersDTO recruiter)
        {
            var result = new ServiceResult();
            var updated = await _adminService.UpdateRecruiter(recruiter);
            if (updated)
            {
                result.SetSuccess();
                result.Message = "successfully updated recruiter.";
            }
            else
            {
                result.SetFailure("could not update recruiter.");
            }
            return result;
        }

        [HttpGet]
        [Route("ExportCandidateListToExcel")]
        public async Task<HttpResponseMessage> ExportAllCandidates()
        {
            var response = new HttpResponseMessage();   
            var exportedBytes = await _adminService.ExportCandidates();
            if(exportedBytes != null)
            {
                response = CreateHttpResponseForExcelExport(exportedBytes, "Candidate List");
            }
            return response;
        }

        [HttpGet]
        [Route("ExportRcruiterList")]
        public async Task<HttpResponseMessage> ExportAllRecruiters()
        {
            var response = new HttpResponseMessage();
            var exportedBytes = _adminService.ExportRecruiters();
            if (exportedBytes != null)
            {
                response = CreateHttpResponseForExcelExport(exportedBytes, "Recruiter List");
            }
            return response;
        }

        [HttpGet]
        [Route("ExportCandidateWithAppliedJobsList")]
        public async Task<HttpResponseMessage> ExportCandidatesWithAppliedJobs()
        {
            var response = new HttpResponseMessage();
            var exportedBytes = await _adminService.ExportCandidateWithAppliedJobs();
            if (exportedBytes != null)
            {
                response = CreateHttpResponseForExcelExport(exportedBytes, "Candidate List With Jobs");
            }
            return response;
        }

        public HttpResponseMessage CreateHttpResponseForExcelExport(byte[] fileContent, string fileName)
        {

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            var httpResponseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, fileContent);
            httpResponseMessage.Content = new ByteArrayContent(fileContent);
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = fileName;
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            return httpResponseMessage;
        }
    }
}
