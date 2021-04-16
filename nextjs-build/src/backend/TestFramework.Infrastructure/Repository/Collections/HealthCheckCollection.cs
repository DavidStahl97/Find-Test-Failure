using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Repository.Collections;
using TestFramework.Domain;
using TestFramework.Infrastructure.Repository.Database;

namespace TestFramework.Infrastructure.Repository.Collections
{
    class HealthCheckCollection : CollectionBase<HealthCheck>, IHealthCheckCollection
    {
        public HealthCheckCollection(RepositoryContext context) : base(context)
        {
        }

        public void Add(HealthCheck healthCheck)
            => AddEntity(healthCheck);

        public ValueTask<(IEnumerable<HealthCheck> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search)
            => FindEntityPageAscendingOrderAsync(pageIndex, pageSize,
                    x => x.Name, search: x => x.Name.Contains(search));

        public Task<IEnumerable<HealthCheck>> GetAllAsync()
            => GetAllEntitesAsync();

        public ValueTask<OneOf<HealthCheck, NotFound>> GetByIdAsync(int id)
            => GetEntityByIdAsync(id);

        public void Remove(HealthCheck healthCheck)
            => RemoveEntity(healthCheck);
    }
}
