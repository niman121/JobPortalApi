using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
