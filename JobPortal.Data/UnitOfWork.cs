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

        public UnitOfWork(JobDbContext jobDbContext)
        {
            _context = jobDbContext;
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
