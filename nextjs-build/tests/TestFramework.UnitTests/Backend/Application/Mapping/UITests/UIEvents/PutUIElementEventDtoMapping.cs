using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public abstract class PutUIElementEventDtoMapping<TSource, TDestination> : PutUIEventDtoMapping<TSource, TDestination>
        where TSource : PutUIElementEventDto, new()
        where TDestination : UIElementEvent
    {
        [Fact]
        public void ShouldMap_WaitForUIElement() => Map(x => x.WaitForUIElement, x => x.WaitForUIElement);

        [Fact]
        public void ShouldMap_UIElementId() => Map(x => x.UIElementId, x => x.UIElementId);

        [Fact]
        public void ShouldMap_UIElement() => Map().UIElement.Should().BeNull();

        [Fact]
        public void ShouldMap_UseDefaultWaitForUIElement() => Map(x => x.UseDefaultWaitForUIElement, x => x.UseDefaultWaitForUIElement);
    }
}
