using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public abstract class GetUIElementEventDtoMapping<TSource, TDestination> 
        : GetUIEventDtoMapping<TSource, TDestination>
        where TSource : UIElementEvent
        where TDestination : GetUIElementEventDto
    {
        [Fact]
        public void ShouldMap_WaitForUIElement() => Map(x => x.WaitForUIElement, x => x.WaitForUIElement);

        [Fact]
        public void ShouldMap_UseDefaultWaitForUIElement() => Map(x => x.UseDefaultWaitForUIElement, x => x.UseDefaultWaitForUIElement);

        [Fact]
        public void ShouldMap_UIElement()
        {
            // Arrange
            var uiElement = new UIElement
            {
                Id = Fixture.Create<int>(),
                Name = Fixture.Create<string>()
            };

            var uiElementEvent = Fixture.Create<TSource>();
            uiElementEvent.UIElement = uiElement;

            // Act
            var dto = MappingFactory.CreateMapper().Map<TDestination>(uiElementEvent);

            // Assert
            dto.UIElement.Id.Should().Be(uiElement.Id);
            dto.UIElement.Name.Should().Be(uiElement.Name);
        }
    }
}
