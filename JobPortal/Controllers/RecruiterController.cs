using JobPortal.Service;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using JobPortal.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using FromUriAttribute =  System.Web.Http.FromUriAttribute;

namespace JobPortal.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles ="Recruiter")]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;
        private readonly IAccountService _accountService;

        public RecruiterController(IRecruiterService recruiterService, IAccountService accountService)
        {
            _recruiterService = recruiterService;
            _accountService = accountService;
        }

        [Route("PostJob")]
        [HttpPost]
        [ValidateModel]
        public async Task<ServiceResult> AddNewJob(AddNewJobDTO job)
        {
            var result = new ServiceResult();
            var jobAdded = await _recruiterService.AddNewJob(job);
            if (jobAdded)
            {
                result.SetSuccess();
                result.Message = "Job Added Successfully";
            }
            else
            {
                result.SetFailure("Could Not Add job");
            }
            return result;
        }

        [HttpGet]
        [Route("{recruiterId}/jobs/candidates")]
        public async Task<ServiceResult<IEnumerable<CandidateJobDTO>>> SeeCandidatesAppliedToJobs([FromUri] int recruiterId, int from =0 ,int to = 10)
        {
            var result = new ServiceResult<IEnumerable<CandidateJobDTO>>();
            var candidateList = await _recruiterService.SeeCandidateList(recruiterId,from,to);
            if(candidateList != null)
            {
                result.SetSuccess(candidateList);
            }
            return result;
        }

        [HttpGet]
        [Route("{jobId}/Candidates")]
        public async Task<ServiceResult<CandidateJobDTO>> GetJobWithCandidate(int jobId)
        {
            var result = new ServiceResult<CandidateJobDTO>();
            var candidateJob = await _recruiterService.GetCandidateBasedOnJob(jobId);
            if(candidateJob != null)
            {
                result.SetSuccess(candidateJob);
            }
            return result;
        }
    }
}
