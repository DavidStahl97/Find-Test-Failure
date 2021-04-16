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
    public class GetUIPageDtoMapping : MappingTestBase<Page, GetUIPageDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);
    }
}
