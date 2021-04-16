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
    public abstract class PutUIEventDtoMapping<TSource, TDestination> : MappingTestBase<TSource, TDestination>
        where TSource : PutUIEventDto, new()
        where TDestination : UIEvent
    {
        [Fact]
        public void InheritanceMappingTest() => TestInheritance<UIEvent>();

        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_UITestCaseId() => Map().UITestCaseId.Should().Be(0);

        [Fact]
        public void ShouldMap_UITestCase() => Map().UITestCase.Should().BeNull();

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Step() => Map(x => x.Step, x => x.Step);

        [Fact]
        public void ShouldMap_TypeDiscriminator() => new TSource().TypeDiscriminator.Should().Be(TypeDiscriminator);

        protected abstract string TypeDiscriminator { get; }
    }
}
