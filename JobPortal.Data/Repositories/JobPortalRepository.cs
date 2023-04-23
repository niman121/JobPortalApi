using JobPortal.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<T> GetByIdAsync(int id)
        {
            return await _jobDbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<T,bool>> predicate)
        {
            return await _jobDbContext.Set<T>().AnyAsync(predicate);
        }
        public virtual IQueryable<T> Query(bool eager = false)
        {
            var query = _jobDbContext.Set<T>().AsQueryable();
            if (eager)
            {
                foreach (var property in _jobDbContext.Model.FindEntityType(typeof(T)).GetNavigations())
                    query = query.Include(property.Name);
            }
            return query;
        }
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T,bool>> predicate, bool eager = false)
        {
            return await Query(eager).FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 10)
        {
            IQueryable<T> query = _jobDbContext.Set<T>();
            
            if (filter != null)
                query = query.Where(filter);

            return orderBy != null ? (take == -1 ? orderBy(query) : orderBy(query).Skip(skip).Take(take)) : query;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool eager = false,int skip = 0, int take = 10)
        {
            IQueryable<T> query = _jobDbContext.Set<T>();

            if (eager)
                query = Query(true);

            if (filter != null)
                query = query.Where(filter);

            return orderBy != null ? (take == -1 ? orderBy(query) : orderBy(query).Skip(skip).Take(take)) : query;
        }
    }
}
