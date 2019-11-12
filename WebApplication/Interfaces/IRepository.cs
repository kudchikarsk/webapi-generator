using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication.BaseHelpers;

namespace WebApplication.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<IEnumerable<TEntity>> GetRandom(Expression<Func<TEntity, bool>> filter = null, int limit = 10, string includeProperties = "");
        TEntity GetByID(object id);
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        Task<PagedResult<TEntity>> CreatePagedResults(
            Expression<Func<TEntity, bool>> filter,
            int page,
            int pageSize,
            string orderBy,
            bool ascending,
            string query,
            string includeProperties = null
            );


    }
}
