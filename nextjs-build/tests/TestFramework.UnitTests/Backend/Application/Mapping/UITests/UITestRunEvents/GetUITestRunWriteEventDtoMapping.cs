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
    public class GetUITestRunWriteEventDtoMapping : GetUITestRunUIElementEventDtoMapping<UITestRunWriteEvent, GetUITestRunWriteEventDto>
    {
        protected override string TypeDiscriminator => "GetUITestRunWriteEventDto";

        [Fact]
        public void ShouldMap_Input() => Map(x => x.Input, x => x.Input);

        [Fact]
        public void ShouldMap_GenerateUnique() => Map(x => x.GenerateUnique, x => x.GenerateUnique);
    }
}
