using Gara.Domain;
using Gara.Persistance.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Gara.Persistance.Ef
{
    public interface IEfRepository<TContext, TEntity> : IRepository<TEntity>
        where TEntity : EntityBaseWithId
    {

    }

    public interface IEfRepository<TContext, TEntity, TModel> : IRepository<TEntity, TModel>
        where TContext : DbContext
        where TEntity : EntityBaseWithId
        where TModel : class
    {
    }
}
