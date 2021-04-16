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
    public class UITestRunWriteEventMapping : UITestRunUIElementEventMapping<WriteEvent, UITestRunWriteEvent>
    {
        [Fact]
        public void ShouldMap_Input() => Map(x => x.Input, x => x.Input);

        [Fact]
        public void ShouldMap_GenerateUnique() => Map(x => x.GenerateUnique, x => x.GenerateUnique);
    }
}
