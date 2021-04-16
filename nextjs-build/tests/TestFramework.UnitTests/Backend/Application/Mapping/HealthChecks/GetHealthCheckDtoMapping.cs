using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.HealthChecks;
using TestFramework.Domain;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.HealthChecks
{
    public class GetHealthCheckDtoMapping : MappingTestBase<HealthCheck, GetHealthCheckDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Url() => Map(x => x.Url, x => x.Url);

        [Fact]
        public void ShouldMap_Healthy() => Map(x => x.Healthy, x => x.Healthy);
    }
}
