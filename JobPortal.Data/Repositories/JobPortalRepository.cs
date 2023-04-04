using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Data.Repositories
{
    public class JobPortalRepository<T> : IJobPortalRepository<T> where T : class
    {
        private readonly JobDbContext _jobDbContext;

        public JobPortalRepository(JobDbContext jobDbContext)
        {
            _jobDbContext = jobDbContext;
        }

        public void Add(T entity)
        {
            var dbSet = _jobDbContext.Set<T>();
            dbSet.Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            var dbSet = _jobDbContext.Set<T>();
            await dbSet.AddAsync(entity);
        }

        public Task AttachUpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            _jobDbContext.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _jobDbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
