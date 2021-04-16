using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRuns;
using TestFramework.Domain.UITesting.Run;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRuns
{
    public class GetUITestRunCaseDtoMapping : MappingTestBase<UITestRunCase, GetUITestRunCaseDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Start() => Map(x => x.Start, x => x.Start);

        [Fact]
        public void ShouldMap_Duration() => Map(x => x.Duration, x => x.Duration);

        [Fact]
        public void ShouldMap_State() => Map(x => x.State, x => x.State);

        [Fact]
        public void ShouldMap_Browser() => Map(x => x.Browser, x => x.Browser);
    }
}
