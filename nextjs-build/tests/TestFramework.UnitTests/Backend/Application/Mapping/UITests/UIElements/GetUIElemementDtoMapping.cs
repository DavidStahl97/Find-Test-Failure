using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Domain.UITesting.Template;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIElements
{
    public class GetUIElemementDtoMapping : MappingTestBase<UIElement, GetUIElementDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Name() => Map(x => x.Name, x => x.Name);

        [Fact]
        public void ShouldMap_FindByMethod() => Map(x => x.FindByMethod, x => x.FindByMethod);

        [Fact]
        public void ShouldMap_FindBy() => Map(x => x.FindBy, x => x.FindBy);

        [Fact]
        public void ShouldMap_PageId() => Map(x => x.PageId, x => x.PageId);
    }
}
