using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITests.UITestRuns.Events
{
    public abstract class UITestRunEventMapping<TSource, TDestination> : MappingTestBase<TSource, TDestination>
        where TSource : UIEvent
        where TDestination : UITestRunEvent
    {
        [Fact]
        public void InheritanceMappingTest() => TestInheritance<UITestRunEvent>();

        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_Step() => Map(x => x.Step, x => x.Step);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Start() => Map().Start.Should().BeNull();

        [Fact]
        public void ShouldMap_Duration() => Map().Duration.Should().BeNull();

        [Fact]
        public void ShouldMap_State() => Map().State.Should().Be(UITestEventState.NotStarted);

        [Fact]
        public void ShouldMap_Result() => Map().Result.Should().Be(EventFailure.NotStarted);

        [Fact]
        public void ShouldMap_UIRunCase() => Map().UIRunCase.Should().Be(0);

        [Fact]
        public void ShouldMap_RunCase() => Map().RunCase.Should().BeNull();

        [Fact]
        public void ShouldMap_Logs() => Map().Logs.Should().BeNull();
    }
}
