using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CandidateDTO>> GetAllCandidates(bool? onlyActiveCandidates, int skip, int take)
        {
            var result = new List<CandidateDTO>();
            var activeCandidates = await _unitOfWork.CandidateRepository.GetAllCandidates(onlyActiveCandidates, skip, take);

            foreach (var candidate in activeCandidates)
            {
                var dto = new CandidateDTO();
                dto.CandidateName = candidate.User.Name;
                dto.CandidateId = candidate.Id;
                dto.ModifiedDate = candidate.User.ModifiedDate;
                dto.CreatedDate = candidate.User.CreatedDate;
                dto.Email = candidate.User.Email;
                result.Add(dto);
            }
            return result;
        }

        public async Task<List<RecruitersDTO>> GetAllRecruiters(bool? onlyActiveRecruiters)
        {
            var result = new List<RecruitersDTO>();
            var recruiters = new List<Recruiter>();

            if (onlyActiveRecruiters.HasValue)
                recruiters = _unitOfWork.RecruiterRepository.GetAll(q => q.IsActive == onlyActiveRecruiters,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();
            else
                recruiters = _unitOfWork.RecruiterRepository.GetAll(null,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();

            if (recruiters.Count > 0)
            {
                foreach (var r in recruiters)
                {
                    var recruiter = new RecruitersDTO();
                    recruiter.RecruiterId = r.Id;
                    recruiter.RecruiterName = r.User.Name;
                    recruiter.CreatedDate = r.User.CreatedDate;
                    recruiter.ModifiedDate = r.User.ModifiedDate;
                    recruiter.Email = r.User.Email;

                    result.Add(recruiter);
                }
            }

            return result;
        }

        public async Task<List<JobDTO>> GetAllJobs(bool? onlyActiveJobs)
        {
            var result = new List<JobDTO>();
            var jobs = new List<Job>();

            if (onlyActiveJobs.HasValue)
                jobs = _unitOfWork.JobRepository.GetAll(q => q.IsActive == onlyActiveJobs,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();
            else
                jobs = _unitOfWork.JobRepository.GetAll(null,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();

            if (jobs.Count > 0)
            {
                foreach (var j in jobs)
                {
                    var job = new JobDTO();

                    job.Salary = j.Salary;
                    job.Name = j.Name;
                    job.NumberOfOpenings = j.NumberOfOpenings;
                    job.PostedDate = j.CreatedDate;
                    job.Location = j.Location;
                    job.Description = j.Description;
                    job.JobId = j.Id;
                    job.Type = j.JobType;

                    result.Add(job);
                }
            }

            return result;
        }

        public async Task<List<CandidateJobDTO>> GetAllCandidateWithAllJobs(bool? onlyActiveCandidate)
        {
            var result = new List<CandidateJobDTO>();
            var jobs = new List<Job>();
            var candidates = new List<Candidate>();
            //first i have to take list of candidates based on active or deactive
            //then get jobs of each candidates
            //map them to candidatejob dto

            if (onlyActiveCandidate.HasValue)
            {
                candidates = _unitOfWork.CandidateRepository.GetAll(q => q.IsActive == onlyActiveCandidate,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();

                Parallel.ForEach(candidates, async c =>
                {
                    var cJobDto = new CandidateJobDTO();
                    var cJobs = await _unitOfWork.CandidateRepository.GetJobsByCandidateId(c.Id);
                    




                });
                jobs = _unitOfWork.JobRepository.GetAll(q => q.IsActive == onlyActiveCandidate,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();
            }
            else
                jobs = _unitOfWork.JobRepository.GetAll(null,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();

        }
    }
}
