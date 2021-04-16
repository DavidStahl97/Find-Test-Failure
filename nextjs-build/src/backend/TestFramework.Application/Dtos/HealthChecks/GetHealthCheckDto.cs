using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.HealthChecks
{
    public class GetHealthCheckDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri Url { get; set; }

        public bool Healthy { get; set; }
    }
}
