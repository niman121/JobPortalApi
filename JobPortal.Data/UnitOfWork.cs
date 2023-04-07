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
        
        public UnitOfWork(JobDbContext jobDbContext
                         ,IJobPortalUserRepository userRepository)
        {
            _context = jobDbContext;
            UserRepository = userRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
