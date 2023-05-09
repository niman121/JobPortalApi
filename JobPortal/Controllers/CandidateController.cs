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
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace JobPortal.Controllers
{
    [Route("candidate")]
    [Authorize]
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
        [AllowAnonymous]
        public ServiceResult<IEnumerable<JobDTO>> GetJobLists([FromUri]int from = 0, [FromUri]int to = 100)
        {
            var result = new ServiceResult<IEnumerable<JobDTO>>();
            var jobs = _candidateService.GetAllJobs(from, to);
            if (jobs != null) result.SetSuccess(jobs);
            else result.SetFailure("could not load job list");
            return result;
        }

        [Route("{candidateId}/jobs/applications")]
        [HttpPost]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<ServiceResult> ApplyToJobs(int candidateId, int[] applicationIds)
        {
            var result = new ServiceResult();
            var applied = await _candidateService.ApplyToJobs(candidateId,applicationIds);
            if (applied)
            {
                result.SetSuccess();
                result.Message = "Jobs Applied Successfully";
            }
            return result;
        }

        [Route("{candidateId}/appliedjobs")]
        [HttpGet]
        public async Task<ServiceResult<List<JobDTO>>> appliedjobs(int candidateId)
        {
            var result = new ServiceResult<List<JobDTO>>();
            var jobs = await _candidateService.AppliedJobs(candidateId);
            if (jobs.Count > 0)
            {
                result.SetSuccess();
                result.Data = jobs;
            }
            return result;
        }
    }
}
