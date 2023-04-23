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
            if (job != null)
            {
                var newJob = new Job();
                newJob.Salary = job.Salary;
                newJob.Description = job.Description;
                newJob.Name = job.Name;
                newJob.NumberOfOpenings = job.NumberOfOpenings;
                newJob.JobType = job.Type;
                newJob.CreatedDate = DateTime.Now;
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

        public async Task<CandidateJobDTO> GetCandidateBasedOnJob(int jobId)
        {
            var result = new CandidateJobDTO();
            var job = await _unitOfWork.JobRepository.GetByIdAsync(jobId);

            var candidates = await _unitOfWork.CandidateRepository.GetCandidateBasedOnJobId(jobId);
            if (candidates.Count > 0)
            {
                var cjdto = new CandidatesOfJobDto();
                result.JobId = jobId;
                result.JobName = job.Name;
                foreach (var candidate in candidates)
                {
                    cjdto.CandidateId = candidate.Id;
                    cjdto.CandidateName = candidate.User.Name;
                }
            }
            return result;
        }

        public async Task<IEnumerable<CandidateJobDTO>> SeeCandidateList(int recruiterId, int from, int to)
        {
            IEnumerable<CandidateJobDTO> result = null;
            if (recruiterId > 0)
            {
                var cjDTO = new List<CandidateJobDTO>();
                var jobs = _unitOfWork.JobRepository.GetAll(q => q.CreatedBy == recruiterId,
                                                  o => o.OrderByDescending(q => q.CreatedDate), from, to).ToList();

                foreach (var job in jobs)
                {
                    var CJ = new CandidateJobDTO();
                    var candidateDto = new List<CandidatesOfJobDto>();
                    var candidates = await _unitOfWork.CandidateRepository.GetCandidateBasedOnJobId(job.Id);

                    foreach (var candidate in candidates)
                    {
                        var candidateJobDTO = new CandidatesOfJobDto();
                        candidateJobDTO.CandidateId = candidate.Id;
                        candidateJobDTO.CandidateName = candidate.User.Name;
                        candidateDto.Add(candidateJobDTO);
                    }

                    CJ.JobName = job.Name;
                    CJ.JobId = job.Id;
                    CJ.Candidates = candidateDto;
                    cjDTO.Add(CJ);
                }
                result = cjDTO.AsEnumerable();
            }
            return result;
        }
    }
}
