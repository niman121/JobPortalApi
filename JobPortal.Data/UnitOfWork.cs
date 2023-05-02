using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly JobDbContext _context;
        public IJobPortalUserRepository UserRepository { get; }
        public IJobPortalOtpRepository OtpRepository { get; }
        public IJobPortalJobRepository JobRepository { get; }
        public IJobPortalCandidateRepository CandidateRepository { get; }   
        public IJobPortalAdminRepository AdminRepository { get; }   
        public IJobPortalRecruiterRepository RecruiterRepository { get; }
        public IJobPortalRoleRepository RoleRepository { get; }

        public UnitOfWork(JobDbContext jobDbContext
                         , IJobPortalUserRepository userRepository
                         , IJobPortalOtpRepository otpRepository
                         , IJobPortalJobRepository jobRepository
                         , IJobPortalRecruiterRepository recruiterRepository
                         , IJobPortalAdminRepository adminRepository
                         , IJobPortalCandidateRepository candidateRepository
                         , IJobPortalRoleRepository roleRepository)
        {
            _context = jobDbContext;
            UserRepository = userRepository;
            OtpRepository = otpRepository;
            JobRepository = jobRepository;  
            RecruiterRepository = recruiterRepository;
            AdminRepository = adminRepository;
            CandidateRepository = candidateRepository;
            RoleRepository = roleRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
