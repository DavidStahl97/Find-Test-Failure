using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.HealthChecks;

namespace TestFramework.Application.Requests.HealthChecks
{
    public class PutRequest : IChangeOrCreate
    {
        public int Id { get; init; }

        public ChangeOrCreateHealthCheckDto Dto { get; init; }
    }
}
