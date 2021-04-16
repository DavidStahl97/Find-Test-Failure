using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public class GetUIElementFromEventDtoMapping : MappingTestBase<UIElement, GetUIElementFromEvent>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Page()
        {
            // Arrange
            var uiElement = Fixture.Create<UIElement>();
            uiElement.Page = Fixture.Create<Page>();

            // Act
            var dto = MappingFactory.CreateMapper().Map<GetUIElementFromEvent>(uiElement);

            // Assert
            dto.Page.Id.Should().Be(uiElement.Page.Id);
        }
    }
}
