using JobPortal.Data.Models;
using JobPortal.Service;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using JobPortal.Utility;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace JobPortal.Controllers
{
    [Route("[controller]")]
    [Authorize(Roles = "Candidate")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [Route("joblist")]
        [HttpGet]
        public ServiceResult<IEnumerable<JobDTO>> GetJobLists(int from = 0, int to = 100)
        {
            var result = new ServiceResult<IEnumerable<JobDTO>>();
            var jobs = _candidateService.GetAllJobs(from, to);
            if (jobs != null) result.SetSuccess(jobs);
            else result.SetFailure("could not load job list");
            return result;
        }

        [Route("applyjobs")]
        [System.Web.Http.HttpPost]
        [ValidateModel]
        public async Task<ServiceResult> ApplyToJobs(ApplyJobDTO jobs)
        {
            var result = new ServiceResult();
            var applied = await _candidateService.ApplyToJobs(jobs);
            if (applied)
            {
                result.SetSuccess();
                result.Message = "Jobs Applied Successfully";
            }
            return result;
        }

        [Route("appliedjobs")]
        [HttpGet]
        public async Task<ServiceResult<List<JobDTO>>> AppliedJobs(int userId)
        {
            var jobs = new List<JobDTO>();
            return jobs;
        }
    }
}
