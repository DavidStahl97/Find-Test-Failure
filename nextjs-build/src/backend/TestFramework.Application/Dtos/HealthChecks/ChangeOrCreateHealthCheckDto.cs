using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.HealthChecks
{
    public class ChangeOrCreateHealthCheckDto
    {
        public string Name { get; set; }

        public Uri Url { get; set; }
    }
}
