using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using X.PagedList;

using helping_hand.Models;

namespace helping_hand.Data.IRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );

        Task<IPagedList<T>> GetAll(
            RequestParams requestParams,
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null
        );

        Task<T> Get(Expression<Func<T, bool>> expression = null, List<string> includes = null);

        Task Insert(T entity);
        Task InsertRange(IEnumerable<T> entities);

        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);

        void Update(T entity);
    }
}
