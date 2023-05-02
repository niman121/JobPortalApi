using JobPortal.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        IJobPortalUserRepository UserRepository { get; }    
        IJobPortalOtpRepository OtpRepository { get; }
        IJobPortalJobRepository JobRepository { get; }
        public IJobPortalCandidateRepository CandidateRepository { get; }
        public IJobPortalAdminRepository AdminRepository { get; }
        public IJobPortalRecruiterRepository RecruiterRepository { get; }
        public IJobPortalRoleRepository RoleRepository { get; }

    }
}
