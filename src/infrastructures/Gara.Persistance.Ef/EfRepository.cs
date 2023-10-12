using Gara.Domain;
using System.Linq.Expressions;

namespace Gara.Persistance.Ef
{
    public class EfRepository<TContext, TEntity> : IEfRepository<TContext, TEntity>
        where TEntity : EntityBaseWithId
    {
        private readonly TContext DbContext;

        public EfRepository(TContext dbContent)
        {
            DbContext = dbContent;
        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AddAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count(Expression<Func<TEntity, bool>> whereCondition = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAsync(int page = 0, int numberItemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetWithIncludeAsync(Expression<Func<TEntity, bool>> whereCondition, int page = 0, int numberItemsPerPage = 0, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> whereCondition, int page = 0, int numberItemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangeAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
