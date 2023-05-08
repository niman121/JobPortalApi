using JobPortal.Data;
using JobPortal.Service.Dtos;
using JobPortal.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobPortal.Data.Models;

namespace JobPortal.Service.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public CandidateService(IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }
        public async Task<List<JobDTO>> AppliedJobs(int candidateId)
        {
            var joblist = new List<JobDTO>();
            var candidate = await _unitOfWork.CandidateRepository.GetByIdAsync(candidateId);
            var jobs =  await _unitOfWork.CandidateRepository.GetJobsByCandidateId(candidateId);
            jobs.Select(s => new JobDTO
            {
                JobId = s.Id,
                Description = s.Description,
                Location = s.Location,
                Name = s.Name,
                PostedDate = s.CreatedDate,
                Salary = s.Salary,
                Type = s.JobType

            });
            return joblist;
        }

        public async Task<bool> ApplyToJobs(int candidateId, int[] applicationIds)
        {
            bool status = false;
            var candidate = await _unitOfWork.CandidateRepository.GetFirstOrDefaultAsync(q => q.Id == candidateId, true);
            var candidateUser = candidate.User;
            if (candidate != null && applicationIds.Length > 0)
            {
                var candidateJob = await _unitOfWork.CandidateRepository.AddJobs(candidate.Id, applicationIds.ToList());

                if (candidateJob > 0)
                {
                    foreach (var id in applicationIds)
                    {
                        var job = await _unitOfWork.JobRepository.GetByIdAsync(id);
                        status = true;
                        var candidateMail = PrepareMailMessageForCandidate(job.Name, candidateUser.Email);
                        var recruiterMail = PrepareMailMessageForRecruiter(candidateUser.Name, job.Name, job.Recruiter.User.Email);
                        _ = Task.Run(() =>
                        {
                            _emailService.SendEmailAsync(candidateMail);
                            _emailService.SendEmailAsync(recruiterMail);
                        });
                    }
                    status = true;
                }
            }
            return status;
        }

        public IEnumerable<JobDTO> GetAllJobs(int skip, int take)
        {
            var jobs = _unitOfWork.JobRepository.GetAll(q => q.IsActive == true, s => s.OrderByDescending(o => o.CreatedDate), skip, take)
                        .Select(s => new JobDTO
                        {
                            JobId = s.Id,
                            Name = s.Name,
                            Description = s.Description,
                            Type = s.JobType,
                            PostedDate = s.CreatedDate,
                            Salary = s.Salary,
                            Location = s.Location,
                        });

            return jobs;
        }


        public static Mail PrepareMailMessageForCandidate(string JobTitle, string email)
        {
            var mail = new Mail();
            mail.Subject = "Job Application Confirmation.";
            mail.Body = "Your Job Application For " + JobTitle + " Has Been Submitted SuccessFully";
            mail.ToEmail.Add(email);
            return mail;
        }
        public static Mail PrepareMailMessageForRecruiter(string user, string JobTitle, string email)
        {
            var mail = new Mail();
            mail.Subject = "Job Application Confirmation.";
            mail.Body = $"{user} Has Applied to job {JobTitle} posted By you";
            mail.ToEmail.Add(email);
            return mail;
        }
    }
}
