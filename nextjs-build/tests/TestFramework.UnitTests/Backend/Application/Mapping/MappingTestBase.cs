using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping
{
    public abstract class MappingTestBase<TSource, TDestination>
    {
        private const string SHOULD_MAP_PREFIX = "ShouldMap_";

        protected IFixture Fixture { get; } = FixtureFactory.GetCustomizedFixture();

        protected virtual IEnumerable<string> IgnoreProperties() =>
            new List<string>();

        protected TDestination Map()
            => MappingFactory.CreateMapper()
                .Map<TSource, TDestination>(CreateSource());

        protected TSource CreateSource() => Fixture.Create<TSource>();

        [Fact]
        public void CheckPropertiesTests()
        {
            var mapTestMethods = GetType().GetMethods()
                .Where(x => x.Name.StartsWith(SHOULD_MAP_PREFIX))
                .ToList();

            foreach (var property in typeof(TDestination).GetProperties())
            { 
                if (IgnoreProperties().Any(x => x == property.Name))
                {
                    continue;
                }

                var hasTestMethod = mapTestMethods
                    .Any(x => x.Name == $"{SHOULD_MAP_PREFIX}{property.Name}");
                if (hasTestMethod == false)
                {
                    throw new Exception($"The mapping of the property {property.Name} has no test");
                }
            }
        }

        protected void Map<TSourceProperty, TDestinationProperty>(
            Action<TSource> configureSource,
            Func<TSource, TSourceProperty> sourceProperty,
            Func<TDestination, TDestinationProperty> destinationProperty)
        {
            var mapper = MappingFactory.CreateMapper();

            var source = Fixture.Create<TSource>();
            configureSource(source);
            var destination = mapper.Map<TDestination>(source);

            destinationProperty(destination).Should().Be(sourceProperty(source));
        }

        protected void Map<TSourceProperty, TDestinationProperty>(
            Func<TSource, TSourceProperty> sourceProperty,
            Func<TDestination, TDestinationProperty> destinationProperty)
            => Map(x => { }, sourceProperty, destinationProperty);
        
        protected void TestInheritance<TBaseClass>()
        {
            // Arrange
            var source = Fixture.Create<TSource>();

            // Act
            var destination = MappingFactory.CreateMapper().Map<TBaseClass>(source);

            // Assert
            destination.Should().BeOfType<TDestination>();
        }
    }
}
