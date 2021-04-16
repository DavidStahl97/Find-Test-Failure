using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRuns
{
    public class UITestRunUIElementMapping : MappingTestBase<UIElement, UITestRunUIElement>
    {
        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_FindByMethod() => Map(x => x.FindByMethod, x => x.FindByMethod);

        [Fact]
        public void ShouldMap_FindBy() => Map(x => x.FindBy, x => x.FindBy);

        [Fact]
        public void ShouldMap_ClickEvents() => Map().ClickEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_WriteEvents() => Map().WriteEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_MoveToUIElements() => Map().MoveToUIElements.Should().BeNull();

        [Fact]
        public void ShouldMap_ClearContentEvents() => Map().ClearContentEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_ImportFileEvents() => Map().ImportFileEvents.Should().BeNull();
    }
}
