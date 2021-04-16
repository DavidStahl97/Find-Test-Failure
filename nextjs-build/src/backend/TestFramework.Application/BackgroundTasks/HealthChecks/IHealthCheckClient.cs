using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.BackgroundTasks.HealthChecks
{
    public interface IHealthCheckClient
    {
        Task<bool> HealthCheck(Uri uri);
    }
}
