using JobPortal.Service;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using JobPortal.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        private readonly IRecruiterService _recruiterService;
        private readonly IAccountService _accountService;

        public RecruiterController(IRecruiterService recruiterService, IAccountService accountService)
        {
            _recruiterService = recruiterService;
            _accountService = accountService;
        }

        [Route("PostNewJob")]
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
        [Route("SeeCandidateListAppliedToAllMyJobs")]
        public async Task<ServiceResult<IEnumerable<CandidateJobDTO>>> SeeCandidatesAppliedToJobs(int recruiterId, int from,int to)
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
        [Route("GetCandidatesOfJob")]
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
