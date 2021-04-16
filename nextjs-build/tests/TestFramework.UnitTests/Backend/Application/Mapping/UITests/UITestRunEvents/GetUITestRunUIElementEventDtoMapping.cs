using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRunEvents
{
    public abstract class GetUITestRunUIElementEventDtoMapping<TSource, TDestination> : GetUITestRunEventDtoMapping<TSource, TDestination>
        where TSource : UITestRunUIElementEvent
        where TDestination : GetUITestRunUIElementEventDto
    {
        [Fact]
        public void ShouldMap_WaitForUIElement() => Map(x => x.WaitForUIElement, x => x.WaitForUIElement);

        [Fact]
        public void ShouldMap_UIElement()
        {
            var uiElementEvent = Fixture.Create<TSource>();
            uiElementEvent.UIElement = Fixture.Create<UITestRunUIElement>();

            var dto = MappingFactory.CreateMapper().Map<TDestination>(uiElementEvent);

            dto.UIElement.Id.Should().Be(uiElementEvent.UIElement.Id);
            dto.UIElement.Name.Should().Be(uiElementEvent.UIElement.Name);
        }
    }
}
