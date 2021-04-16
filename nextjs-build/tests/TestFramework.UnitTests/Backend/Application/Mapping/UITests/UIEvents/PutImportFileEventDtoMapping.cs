using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public class PutImportFileEventDtoMapping : PutUIElementEventDtoMapping<PutImportFileEventDto, ImportFileEvent>
    {
        protected override string TypeDiscriminator => "PutImportFileEventDto";

        [Fact]
        public void ShouldMap_UserFileId() => Map(x => x.UserFileId, x => x.UserFileId);

        [Fact]
        public void ShouldMap_UserFile() => Map().UserFile.Should().BeNull();
    }
}
