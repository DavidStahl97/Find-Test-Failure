using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIEvents;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UIEvents
{
    public class PutClickEventDtoMapping : PutUIElementEventDtoMapping<PutClickEventDto, ClickEvent>
    {
        protected override string TypeDiscriminator => "PutClickEventDto";
    }
}
