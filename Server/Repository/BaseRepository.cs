using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using X.PagedList;

using helping_hand.Server.Data;
using helping_hand.Server.IRepository;

namespace helping_hand.Server.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            foreach (var prop in includes ?? Enumerable.Empty<string>())
            {
                query = query.Include(prop);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<string> includes = null
        )
        {
            IQueryable<T> query = _dbSet;

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            foreach (var prop in includes ?? Enumerable.Empty<string>())
            {
                query = query.Include(prop);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IPagedList<T>> GetAll(
            RequestParams requestParams,
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<string> includes = null
        )
        {
            IQueryable<T> query = _dbSet;

            if (expression is not null)
            {
                query = query.Where(expression);
            }

            foreach (var prop in includes ?? Enumerable.Empty<string>())
            {
                query = query.Include(prop);
            }

            if (orderBy is not null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToPagedListAsync(requestParams.PageNumber, requestParams.PageSize);
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
