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
    public class GetUITestRunMoveByOffsetEventDtoMapping : GetUITestRunEventDtoMapping<UITestRunMoveByOffsetEvent, GetUITestRunMoveByOffsetEventDto>
    {
        protected override string TypeDiscriminator => "GetUITestRunMoveByOffsetEventDto";

        [Fact]
        public void ShouldMap_OffsetX() => Map(x => x.OffsetX, x => x.OffsetX);

        [Fact]
        public void ShouldMap_OffsetY() => Map(x => x.OffsetY, x => x.OffsetY);
    }
}
