using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Domain;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping
{
    public class GetUserFileDtoMapping : MappingTestBase<UserFile, GetUserFileDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_FileName() => Map(x => x.FileName, x => x.FileName);

        [Fact]
        public void ShouldMap_FileSize() => Map(x => x.FileSize, x => x.FileSize);

        [Fact]
        public void ShouldMap_CreatedDateTime() => Map(x => x.CreatedDateTime, x => x.CreatedDateTime);
    }
}
