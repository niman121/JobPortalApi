using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services
{
    public class RecruiterService : IRecruiterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecruiterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddNewJob(AddNewJobDTO job)
        {
            bool status = false;
            if(job != null)
            {
                var newJob = new Job();
                newJob.Salary = job.Salary; 
                newJob.Description = job.Description;
                newJob.Name= job.Name;
                newJob.NumberOfOpenings = job.NumberOfOpenings;
                newJob.JobType = job.Type;
                newJob.CreatedDate= DateTime.Now;
                newJob.Location = job.Location;
                newJob.CreatedBy = job.CreatedBy;
                newJob.Recruiter = await _unitOfWork.RecruiterRepository.GetByIdAsync(job.CreatedBy);
                newJob.IsActive = true; 
                await _unitOfWork.JobRepository.AddAsync(newJob);
                var rowSaved = await _unitOfWork.CommitAsync();
                if (rowSaved > 0) status = true;
            }
            return status;
        }

        //public async Task<CandidateJobDTO> GetCandidateBasedOnJob(int jobId)
        //{
        //    var job = await _unitOfWork.JobRepository.GetFirstOrDefaultAsync(q => q.Id == jobId,true);
        //    if(job != null)
        //    {
        //        var jobCandidate = job.Candidates.ToList().Select(s =>
                
                
                
        //    }
        //}

        public IEnumerable<CandidateJobDTO> SeeCandidateList(int recruiterId, int from, int to)
        {
            IEnumerable<CandidateJobDTO> result = null;
            if(recruiterId > 0)
            {
                //var jobCandidate = _unitOfWork.JobRepository.GetAll(q => q.CreatedBy == recruiterId,
                //                                  o => o.OrderByDescending(q => q.CreatedDate), from, to)
                //                                        .Select(s => new CandidateJobDTO {
                //                                            JobId = s.Id,
                //                                            JobName = s.Name,
                //                                            Candidates = s.Candidates.Select(m => new CandidatesOfJobDto
                //                                            {
                //                                                CandidateId = m.Id,
                //                                                CandidateName = m.User.Name
                //                                            }).ToList()
                //                                        });
                //if(jobCandidate.Count() > 0)
                //{
                //    result = jobCandidate;
                //}
            }
            return result;
        }
    }
}
