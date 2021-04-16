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
    public abstract class GetUITestRunEventDtoMapping<TSource, TDestination> : MappingTestBase<TSource, TDestination>
        where TSource : UITestRunEvent
        where TDestination : GetUITestRunEventDto
    {
        [Fact]
        public void InheritanceMappingTest() => TestInheritance<GetUITestRunEventDto>();

        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Start() => Map(x => x.Start, x => x.Start);

        [Fact]
        public void ShouldMap_Duration() => Map(x => x.Duration, x => x.Duration);

        [Fact]
        public void ShouldMap_Step() => Map(x => x.Step, x => x.Step);

        [Fact]
        public void ShouldMap_State() => Map(x => x.State, x => x.State);

        [Fact]
        public void ShouldMap_Result() => Map(x => x.Result, x => x.Result);

        [Fact]
        public void ShouldMap_Logs() => Map(x => x.Logs.Count, x => x.Logs.Count());

        [Fact]
        public void ShouldMap_TypeDiscriminator() => Map().TypeDiscriminator.Should().Be(TypeDiscriminator);

        protected abstract string TypeDiscriminator { get; }
    }
}
