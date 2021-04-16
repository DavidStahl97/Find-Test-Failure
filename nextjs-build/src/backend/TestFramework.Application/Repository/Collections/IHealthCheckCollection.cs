using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain;

namespace TestFramework.Application.Repository.Collections
{
    public interface IHealthCheckCollection
    {
        void Add(HealthCheck healthCheck);

        void Remove(HealthCheck healthCheck);

        ValueTask<OneOf<HealthCheck, NotFound>> GetByIdAsync(int id);

        ValueTask<(IEnumerable<HealthCheck> Page, int Count)> FindPageAsync(int pageIndex, int pageSize, string search);

        Task<IEnumerable<HealthCheck>> GetAllAsync();
    }
}
