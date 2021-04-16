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
    public class GetUITestRunWaitEventDtoMapping : GetUITestRunEventDtoMapping<UITestRunWaitEvent, GetUITestRunWaitEventDto>
    {
        protected override string TypeDiscriminator => "GetUITestRunWaitEventDto";

        [Fact]
        public void ShouldMap_Ticks() => Map(x => x.Ticks, x => x.Ticks);
    }
}
