using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.Repository.Collections;
using TestFramework.Application.Types;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    abstract class CollectionBase<TEntity>
        where TEntity : class
    {
        public CollectionBase(RepositoryContext context)
        {
            Context = context;
        }

        protected RepositoryContext Context { get; }

        protected async ValueTask<OneOf<TEntity, NotFound>> GetEntityByIdAsync(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            return Types.CreateNotFound(entity);
        }

        protected async ValueTask<TrueOrFalse> AnyAsync(Expression<Func<TEntity, bool>> filter)
            => await Context.Set<TEntity>().AnyAsync(filter);

        protected void AddEntity(TEntity entity)
            => Context.Set<TEntity>().Add(entity);

        protected void AddRangeEntites(IEnumerable<TEntity> entities)
            => Context.Set<TEntity>().AddRange(entities);

        protected void RemoveEntity(TEntity entity)
            => Context.Set<TEntity>().Remove(entity);

        protected void RemoveRangeEntities(IEnumerable<TEntity> entities)
            => Context.Set<TEntity>().RemoveRange(entities);

        protected async Task<IEnumerable<TEntity>> GetAllEntitesAsync()
            => await Context.Set<TEntity>().ToListAsync();

        protected ValueTask<(IEnumerable<TEntity> Page, int Count)> FindEntityPageDescendingOrderAsync<TOrderBy>(int pageIndex,
            int pageSize, Expression<Func<TEntity, TOrderBy>> orderBy, bool tracking = false,
            Expression<Func<TEntity, bool>> search = null)
            => FindEntityPageAsync<TOrderBy>(pageIndex, pageSize,
                    query => query.OrderByDescending(orderBy), tracking: tracking, search: search);

        protected ValueTask<(IEnumerable<TEntity> Page, int Count)> FindEntityPageAscendingOrderAsync<TOrderBy>(
            int pageIndex, int pageSize, Expression<Func<TEntity, TOrderBy>> orderBy, bool tracking = false,
            Expression<Func<TEntity, bool>> search = null)
            => FindEntityPageAsync<TOrderBy>(pageIndex, pageSize,
                    query => query.OrderBy(orderBy), tracking: tracking, search: search);

        protected async ValueTask<(IEnumerable<TEntity> Page, int Count)> FindEntityPageAsync<TOrderBy>(int pageIndex,
           int pageSize, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, bool tracking = false,
           Expression<Func<TEntity, bool>> search = null)
        {
            var query = Context.Set<TEntity>()
                .AddTracking(tracking);

            if (search is null == false)
            {
                query = query.Where(search);
            }

            var orderedQuery = orderBy(query);

            var page = await orderedQuery
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await Context.Set<TEntity>()
                .CountAsync();

            return (page, count);
        }
    }
}
