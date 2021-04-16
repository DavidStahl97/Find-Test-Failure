using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIElements
{
    public class ChangeOrCreateUIElementDtoMapping : MappingTestBase<ChangeOrCreateUIElemenDto, UIElement>
    {
        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_Page() => Map().Page.Should().BeNull();

        [Fact]
        public void ShouldMap_ClickEvents() => Map().ClickEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_WriteEvents() => Map().WriteEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_ClearContentEvents() => Map().ClearContentEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_ImportFileEvents() => Map().ImportFileEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_MoveToUIElementEvents() => Map().MoveToUIElementEvents.Should().BeNull();

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_FindByMethod() => Map(x => x.FindByMethod, x => x.FindByMethod);

        [Fact]
        public void ShouldMap_FindBy() => Map(x => x.FindBy, x => x.FindBy);

        [Fact]
        public void ShouldMap_PageId() => Map(x => x.PageId, x => x.PageId);
    }
}
