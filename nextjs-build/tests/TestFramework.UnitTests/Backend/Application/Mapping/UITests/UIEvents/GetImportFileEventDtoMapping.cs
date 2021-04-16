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
    public class GetImportFileEventDtoMapping : GetUIElementEventDtoMapping<ImportFileEvent, GetImportFileEventDto>
    {
        protected override string TypeDiscriminator => "GetImportFileEventDto";

        [Fact]
        public void ShouldMap_UserFile() => Map(x => x.UserFile.FileName, x => x.UserFile.FileName);
    }
}
