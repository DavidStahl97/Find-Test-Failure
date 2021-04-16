using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunCases;
using TestFramework.Domain.UITesting.Run;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRunCases
{
    public class GetUITestRunCaseDetailsDtoMapping : MappingTestBase<UITestRunCase, GetUITestRunCaseDetailsDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_StartUrl() => Map(x => x.StartUrl, x => x.StartUrl);

        [Fact]
        public void ShouldMap_Browser() => Map(x => x.Browser, x => x.Browser);

        [Fact]
        public void ShouldMap_Start() => Map(x => x.Start, x => x.Start);

        [Fact]
        public void ShouldMap_Duration() => Map(x => x.Duration, x => x.Duration);

        [Fact]
        public void ShouldMap_State() => Map(x => x.State, x => x.State);

        [Fact]
        public void ShouldMap_Events() => Map().Events.Should().HaveCountGreaterThan(0);
    }
}
