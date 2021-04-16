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
    public class PutClickAtPositionEventDtoMapping : PutUIEventDtoMapping<PutClickAtPositionEventDto, ClickAtPositionEvent>
    {
        protected override string TypeDiscriminator => "PutClickAtPositionEventDto";
    }
}
