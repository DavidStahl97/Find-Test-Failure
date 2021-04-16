using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.Repository.Collections
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> AddTracking<TEntity>(this IQueryable<TEntity> query, bool shouldTrack)
            where TEntity : class
            => shouldTrack ? query.AsTracking() : query.AsNoTracking();
    }
}
