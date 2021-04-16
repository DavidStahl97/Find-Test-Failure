using FluentAssertions;
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
    public class PutWaitEventDtoMapping : PutUIEventDtoMapping<PutWaitEventDto, WaitEvent>
    {
        protected override string TypeDiscriminator => "PutWaitEventDto";

        [Fact]
        public void ShouldMap_Ticks() => Map(x => x.Step, x => x.Step);
    }
}
