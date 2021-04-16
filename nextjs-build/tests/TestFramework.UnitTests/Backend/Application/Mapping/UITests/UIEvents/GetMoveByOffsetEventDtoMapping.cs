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
    public class GetMoveByOffsetEventDtoMapping : GetUIEventDtoMapping<MoveByOffsetEvent, GetMoveByOffsetEventDto>
    {
        protected override string TypeDiscriminator => "GetMoveByOffsetEventDto";

        [Fact]
        public void ShouldMap_OffsetX() => Map(x => x.OffsetX, x => x.OffsetX);

        [Fact]
        public void ShouldMap_OffsetY() => Map(x => x.OffsetY, x => x.OffsetY);
    }
}
