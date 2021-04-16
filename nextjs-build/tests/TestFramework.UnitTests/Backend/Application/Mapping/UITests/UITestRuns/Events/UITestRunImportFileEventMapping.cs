using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITests.UITestRuns.Events
{
    public class UITestRunImportFileEventMapping : UITestRunUIElementEventMapping<ImportFileEvent, UITestRunImportFileEvent>
    {
        [Fact]
        public void ShouldMap_FileName() => Map(x => x.UserFile.FileName, x => x.FileName);

        [Fact]
        public void ShouldMap_StoredFileName() => Map(x => x.UserFile.StoredFileName, x => x.StoredFileName);
    }
}
