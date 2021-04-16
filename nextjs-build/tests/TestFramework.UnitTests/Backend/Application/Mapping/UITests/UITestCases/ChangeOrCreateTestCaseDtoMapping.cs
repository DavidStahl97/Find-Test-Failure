using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestCases
{
    public class ChangeOrCreateTestCaseDtoMapping : MappingTestBase<ChangeOrCreateUITestCaseDto, UITestCase>
    {
        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_Events() => Map().Events.Should().BeNull();

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_StartUrl() => Map(x => x.StartUrl, x => x.StartUrl);

        [Fact]
        public void ShouldMap_DefaultWaitForUIElement() => Map(x => x.DefaultWaitForUIElement, x => x.DefaultWaitForUIElement);

        [Fact]
        public void ShouldMap_RunsPeriodically() => Map(x => x.RunsPeriodically, x => x.RunsPeriodically);
    }
}
