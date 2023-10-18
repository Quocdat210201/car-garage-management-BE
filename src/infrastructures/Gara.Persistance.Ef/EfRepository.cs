using AutoMapper;
using Gara.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Gara.Persistance.Ef
{
    public class EfRepository<TContext, TEntity> : IEfRepository<TContext, TEntity>
        where TContext : DbContext
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

        public async Task<IEnumerable<TEntity>> GetAsync(int page = 0, int numberItemsPerPage = 0)
        {
            var result = DbContext.Set<TEntity>().AsQueryable();

            if (page == 0)
            {
                return await result.ToListAsync();
            }

            return await result
                .Skip((page - 1) * numberItemsPerPage)
                .Take(numberItemsPerPage)
                .ToListAsync();
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

    public class EfRepository<TContext, TEntity, TModel> : IEfRepository<TContext, TEntity, TModel>
        where TContext : DbContext
        where TEntity : EntityBaseWithId
        where TModel : class
    {
        protected readonly TContext DbContext;
        private readonly IMapper _mapper;
        public EfRepository(TContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            _mapper = mapper;
        }

        public Task<TModel> AddAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> AddAsync(List<TModel> models)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count(Expression<Func<TEntity, bool>> whereCondition = null)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(List<TModel> models)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAsync(int page = 0, int numberItemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> GetAsync(IQueryable<TEntity> datasource, int page = 0, int numberItemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TModel>> ListAsync(Expression<Func<TModel, bool>> whereCondition, int page = 0, int numberItemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
