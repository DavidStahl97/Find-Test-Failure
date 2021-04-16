using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRunEvents
{
    public class GetUITestRunMoveToUIElementEventMapping : GetUITestRunUIElementEventDtoMapping<UITestRunMoveToUIElementEvent, GetUITestRunMoveToUIElementEventDto>
    {
        protected override string TypeDiscriminator => "GetUITestRunMoveToUIElementEventDto";
    }
}
