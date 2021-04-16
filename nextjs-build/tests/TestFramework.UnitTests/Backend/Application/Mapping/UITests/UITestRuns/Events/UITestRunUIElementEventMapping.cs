using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITests.UITestRuns.Events
{
    public abstract class UITestRunUIElementEventMapping<TSource, TDestination> : UITestRunEventMapping<TSource, TDestination>
        where TSource : UIElementEvent
        where TDestination : UITestRunUIElementEvent
    {
        [Fact]
        public void ShouldMap_UseDefaultWaitForUIElement() => Map(x => x.UseDefaultWaitForUIElement, x => x.UseDefaultWaitForUIElement);

        [Fact]
        public void ShouldMap_WaitForUIElement() => Map(x => x.WaitForUIElement, x => x.WaitForUIElement);

        [Fact]
        public void ShouldMap_UITestRunUIElementId() => Map(x => x.UIElementId, x => x.UITestRunUIElementId);

        [Fact]
        public void ShouldMap_UIElement() => Map().UIElement.Should().BeNull();
    }
}
