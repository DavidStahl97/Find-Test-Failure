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
    public class UITestRunMoveByOffsetEventMapping : UITestRunEventMapping<MoveByOffsetEvent, UITestRunMoveByOffsetEvent>
    {
        [Fact]
        public void ShouldMap_OffsetX() => Map(x => x.OffsetX, x => x.OffsetX);

        [Fact]
        public void ShouldMap_OffsetY() => Map(x => x.OffsetY, x => x.OffsetY);
    }
}
