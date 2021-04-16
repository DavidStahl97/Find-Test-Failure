using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public class GetMoveToUIElementEventDtoMapping : GetUIElementEventDtoMapping<MoveToUIElementEvent, GetMoveToUIElementEventDto>
    {
        protected override string TypeDiscriminator => "GetMoveToUIElementEventDto";
    }
}
