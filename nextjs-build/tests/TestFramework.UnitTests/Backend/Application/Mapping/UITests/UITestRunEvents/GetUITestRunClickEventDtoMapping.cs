using AutoFixture;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITestRunEvents
{
    public class GetUITestRunClickEventDtoMapping : GetUITestRunUIElementEventDtoMapping<UITestRunClickEvent, GetUITestRunClickEventDto>
    {
        protected override string TypeDiscriminator => "GetUITestRunClickEventDto";
    }
}
