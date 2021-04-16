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
    public abstract class GetUIEventDtoMapping<TSource, TDestination> : MappingTestBase<TSource, TDestination>
        where TSource : UIEvent
        where TDestination : GetUIEventDto
    {
        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_Step() => Map(x => x.Step, x => x.Step);

        [Fact]
        public void ShouldMap_TypeDiscriminator() => Map().TypeDiscriminator.Should().Be(TypeDiscriminator);

        protected abstract string TypeDiscriminator { get; }
        
        [Fact]
        public void InheritanceMappingTest() => TestInheritance<GetUIEventDto>();
    }
}
