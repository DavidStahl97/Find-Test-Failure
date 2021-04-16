using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class EventLogDto
    {
        public int Id { get; set; }

        public EventLogLevel LogLevel { get; set; }

        public DateTime? Timestamp { get; set; }

        public string Message { get; set; }
    }
}
