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

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity is IAuditable)
            {
                ((IAuditable)entity).CreatedDate = DateTime.UtcNow;
                ((IAuditable)entity).UpdatedDate = DateTime.UtcNow;
            }
            await DbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddAsync(List<TEntity> entities)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
            return entities;
        }

        public Task<int> Count(Expression<Func<TEntity, bool>> whereCondition = null)
        {
            if (whereCondition == null)
            {
                return DbContext.Set<TEntity>().CountAsync();
            }
            else
            {
                return DbContext.Set<TEntity>().CountAsync(whereCondition);
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Task.Yield();
            DbContext.Set<TEntity>().Remove(entity);
        }

        public async Task DeleteAsync(List<TEntity> entities)
        {
            await Task.Yield();
            DbContext.Set<TEntity>().RemoveRange(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(IEnumerable<string> includes = null, int page = 0, int numberItemsPerPage = 0)
        {
            var result = DbContext.Set<TEntity>().AsQueryable();

            if (includes != null)
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

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
            return DbContext.Set<TEntity>().FindAsync(id).AsTask();
        }

        public async Task<IEnumerable<TEntity>> GetWithIncludeAsync(Expression<Func<TEntity, bool>> whereCondition, int page = 0, int numberItemsPerPage = 0, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = DbContext.Set<TEntity>().AsNoTracking().AsQueryable();
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                result = result.Include(include);
            }

            if (whereCondition != null)
            {
                result = result.Where(whereCondition);
            }

            if (page == 0 && numberItemsPerPage == 0)
            {
                return await result.AsNoTracking().ToListAsync();
            }

            return await result
                .Skip((page - 1) * numberItemsPerPage)
                .Take(numberItemsPerPage)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> whereCondition, int page = 0, int numberItemsPerPage = 0)
        {
            var result = DbContext.Set<TEntity>().AsNoTracking().AsQueryable().Where(whereCondition);
            if (page == 0 && numberItemsPerPage == 0)
            {
                return await result.ToListAsync();
            }

            return await result
                .Skip((page - 1) * numberItemsPerPage)
                .Take(numberItemsPerPage)
                .ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return DbContext.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        public Task<int> SaveChangeAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            if (entity is IAuditable)
            {
                ((IAuditable)entity).UpdatedDate = DateTime.UtcNow;
            }
            return DbContext.SaveChangesAsync();
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
