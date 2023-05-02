using JobPortal.Data;
using JobPortal.Data.Models;
using JobPortal.Service.Dtos;
using JobPortal.Service.Enums;
using JobPortal.Service.Extension;
using JobPortal.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PasswordHashTool;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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

        public async Task<List<CandidatesWithJobsDto>> GetAllCandidateWithAllJobs(bool? onlyActiveCandidate)
        {
            var result = new List<CandidatesWithJobsDto>();
            var jobs = new List<Job>();
            var candidates = new List<Candidate>();

            if (onlyActiveCandidate.HasValue)
                candidates = _unitOfWork.CandidateRepository.GetAll(q => q.IsActive == onlyActiveCandidate,
                                                            o => o.OrderByDescending(s => s.Id), true, 0, 100).ToList();
            else
                candidates = _unitOfWork.CandidateRepository.GetAll(null, o => o.OrderByDescending(q => q.Id), true, 0, 100).ToList();


            if (candidates.Count > 0)
            {
                Parallel.ForEach(candidates, async c =>
                {
                    var cJobDto = new CandidatesWithJobsDto();
                    var cJobs = await _unitOfWork.CandidateRepository.GetJobsByCandidateId(c.Id);

                    cJobDto.CandidateId = c.Id;
                    cJobDto.CandidateName = c.User.Name;
                    foreach (var job in cJobs)
                    {
                        var j = new JobDTO();
                        j.JobId = job.Id;
                        j.PostedDate = job.CreatedDate;
                        j.NumberOfOpenings = job.NumberOfOpenings;
                        j.Name = job.Name;
                        j.Description = job.Description;
                        j.Location = job.Location;
                        j.Salary = job.Salary;
                        j.Type = job.JobType;
                        cJobDto.jobs.Add(j);
                    }
                    result.Add(cJobDto);
                });
            }
            return result;
        }

        public async Task<CandidatesWithJobsDto> GetCandidateJobs(int candidateId)
        {
            var result = new CandidatesWithJobsDto();

            var candidate = await _unitOfWork.CandidateRepository.GetByIdAsync(candidateId);
            var jobs = await _unitOfWork.CandidateRepository.GetJobsByCandidateId(candidateId);

            result.CandidateId = candidate.Id;
            result.CandidateName = candidate.User.Name;

            Parallel.ForEach(jobs, job =>
            {
                var jobdto = new JobDTO();
                jobdto.Location = job.Location;
                jobdto.Name = job.Name;
                jobdto.Description = job.Description;
                jobdto.PostedDate = job.CreatedDate;
                jobdto.JobId = job.Id;
                jobdto.Salary = job.Salary;
                jobdto.NumberOfOpenings = job.NumberOfOpenings;
                jobdto.Type = job.JobType;
                result.jobs.Add(jobdto);
            });
            return result;
        }

        public async Task<CandidateJobDTO> GetCandidatesByJob(int jobId)
        {
            var result = new CandidateJobDTO();
            var job = await _unitOfWork.JobRepository.GetByIdAsync(jobId);

            if (job != null)
            {
                var candidates = await _unitOfWork.CandidateRepository.GetCandidateBasedOnJobId(jobId);

                result.JobId = job.Id;
                result.JobName = job.Name;
                if (candidates.Count > 0)
                {
                    Parallel.ForEach(candidates, c =>
                    {
                        var cdto = new CandidatesOfJobDto();
                        cdto.CandidateId = c.Id;
                        cdto.CandidateName = c.User.Name;
                        result.Candidates.Add(cdto);
                    });
                }
            }

            return result;
        }

        public async Task<bool> deActivateJob(int jobId)
        {
            bool status = false;
            
            await _unitOfWork.JobRepository.DeActivateJob(jobId);
            var rowAffected = await _unitOfWork.CommitAsync();
        
            if (rowAffected > 0) status = true;
            return status;
        }

        public async Task<bool> deActivateCandidate(int candidateId)
        {
            bool status = false;

            await _unitOfWork.CandidateRepository.DeActivateCandidate(candidateId);
            var rowAffected = await _unitOfWork.CommitAsync();

            if (rowAffected > 0) status = true;
            return status;
        }

        public async Task<bool> deActivateRecruiter(int recruiterId)
        {
            bool status = false;

            var recruiter = await _unitOfWork.RecruiterRepository.GetByIdAsync(recruiterId);
            recruiter.IsActive = false;
            _unitOfWork.RecruiterRepository.Update(recruiter);
            var rowAffected = await _unitOfWork.CommitAsync();

            if (rowAffected > 0) status = true;
            return status;
        }

        public async Task<bool> AddNewRecruiter(SignUpDto dTO)
        {
            bool status = false;
            var user = new User();
            var recruiter = new Recruiter();
            
            var role = await  _unitOfWork.RoleRepository.GetFirstOrDefaultAsync(q => q.Name == RolesEnum.Recruiter.ToString());
            
            user.Email = dTO.EmailAddress;
            user.CreatedDate = DateTime.Now;
            user.Roles.Add(role);
            user.Password = PasswordHashManager.CreateHash(dTO.Password);
            
            recruiter.User = user;
            recruiter.IsActive = true;
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.RecruiterRepository.AddAsync(recruiter);

            var rowSaved = await _unitOfWork.CommitAsync();

            if(rowSaved > 0)
                status = true;
            
            return status;
        }

        public async Task<bool> UpdateRecruiter(RecruitersDTO rDto)
        {
            bool status = false;

            var recruiter = await _unitOfWork.RecruiterRepository.GetFirstOrDefaultAsync(q => q.Id == rDto.RecruiterId,true);
            recruiter.User.Name = rDto.RecruiterName;
            recruiter.User.ModifiedDate = DateTime.Now;

             _unitOfWork.RecruiterRepository.Update(recruiter);
            var rowUpdated = await _unitOfWork.CommitAsync();
            if(rowUpdated > 0) status = true;
            return status;
        }

        public async Task<byte[]> ExportCandidates()
        {
            var candidates = await _unitOfWork.CandidateRepository.GetAllCandidates(true, 0, 1000);
            DataTable dt = candidates.ToDataTable();

            var sheets = new List<MultipleSheetsDto> {
                new MultipleSheetsDto
                {
                    dt = dt,
                    ReportHeading = "Candidate List",
                    WorkSheetName = "Candidates List"
                }
            };

            var bytes = MultimediaExt.GetBytesForExportToExcel_MultipleSheets(sheets);

            return bytes;
        }

        public byte[] ExportRecruiters()
        {
            var recruiters = _unitOfWork.RecruiterRepository.GetAll(q => q.IsActive == true, o => o.OrderByDescending(o => o.Id), 0, 1000).ToList();
            DataTable dt = recruiters.ToDataTable();

            var sheets = new List<MultipleSheetsDto> {
                new MultipleSheetsDto
                {
                    dt = dt,
                    ReportHeading = "Recruiter List",
                    WorkSheetName = "Recruiters List"
                }
            };

            var bytes = MultimediaExt.GetBytesForExportToExcel_MultipleSheets(sheets);

            return bytes;
        }

        public async Task<byte[]> ExportCandidateWithAppliedJobs()
        {
            var candidates = await GetAllCandidateWithAllJobs(true);
            DataTable dt = candidates.ToDataTable();

            var sheets = new List<MultipleSheetsDto> {
                new MultipleSheetsDto
                {
                    dt = dt,
                    ReportHeading = "Candidate With Applied Jobs List",
                    WorkSheetName = "Candidates List"
                }
            };

            var bytes = MultimediaExt.GetBytesForExportToExcel_MultipleSheets(sheets);

            return bytes;
        }
    }
}
