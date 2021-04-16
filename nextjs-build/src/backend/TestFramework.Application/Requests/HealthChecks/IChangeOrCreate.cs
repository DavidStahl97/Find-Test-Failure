using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.HealthChecks;

namespace TestFramework.Application.Requests.HealthChecks
{
    public interface IChangeOrCreate
    {
        ChangeOrCreateHealthCheckDto Dto { get; init; }
    }
}
