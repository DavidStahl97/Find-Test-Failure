using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public class GetClickAtPositionEventDtoMapping : GetUIEventDtoMapping<ClickAtPositionEvent, GetClickAtPositionEventDto>
    {
        protected override string TypeDiscriminator => "GetClickAtPositionEventDto";
    }
}
