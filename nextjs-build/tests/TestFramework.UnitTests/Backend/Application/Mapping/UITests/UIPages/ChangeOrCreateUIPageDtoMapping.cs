using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIPages
{
    public class ChangeOrCreateUIPageDtoMapping : MappingTestBase<ChangeOrCreateUIPageDto, Page>
    {
        [Fact]
        public void ShouldMap_Id() => Map().Id.Should().Be(0);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_UIElements() => Map().UIElements.Should().BeNull();
    }
}
