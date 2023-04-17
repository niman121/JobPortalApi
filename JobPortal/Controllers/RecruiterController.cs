using JobPortal.Service;
using JobPortal.Service.Dtos;
using JobPortal.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruiterController : ControllerBase
    {
        [Route("PostNewJob")]
        [HttpPost]
        [ValidateModel]
        public async Task<ServiceResult> AddNewJob(JobDTO job)
        {
            var result = new ServiceResult();
            return result;
        }

        [HttpGet]
        [Route("SeeCandidateListAppliedToJobs")]
        public async Task<ServiceResult<List<CandidateDTO>>> SeeCandidatesAppliedToJobs()
        {
            var result = new List<CandidateDTO>();
            return result;
        } 
     }
}
