using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestRunEvents;
using TestFramework.Domain.UITesting.Run.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.Mapping.UITests.UITestRunEvents
{
    public class EventLogDtoMapping : MappingTestBase<EventLog, EventLogDto>
    {
        [Fact]
        public void ShouldMap_Id() => Map(x => x.Id, x => x.Id);

        [Fact]
        public void ShouldMap_Message() => Map(x => x.Message, x => x.Message);

        [Fact]
        public void ShouldMap_Timestamp() => Map(x => x.Timestamp, x => x.Timestamp);

        [Fact]
        public void ShouldMap_LogLevel() => Map(x => x.LogLevel, x => x.LogLevel);
    }
}
