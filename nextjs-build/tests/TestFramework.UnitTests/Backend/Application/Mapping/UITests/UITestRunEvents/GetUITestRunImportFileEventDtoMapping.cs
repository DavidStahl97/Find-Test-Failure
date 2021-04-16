using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Domain.UITesting.Run.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRunEvents
{
    public class GetUITestRunImportFileEventDtoMapping : GetUITestRunUIElementEventDtoMapping<UITestRunImportFileEvent, GetUITestRunImportFileEventDto>
    {
        protected override string TypeDiscriminator => "GetUITestRunImportFileEventDto";

        [Fact]
        public void ShouldMap_FileName() => Map(x => x.FileName, x => x.FileName);
    }
}
