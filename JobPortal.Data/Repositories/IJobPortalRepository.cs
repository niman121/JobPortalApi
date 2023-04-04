using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories
{
    public interface IJobPortalRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task AddAsync(T entity);
        Task AttachUpdateAsync(T entity);
    }
}
