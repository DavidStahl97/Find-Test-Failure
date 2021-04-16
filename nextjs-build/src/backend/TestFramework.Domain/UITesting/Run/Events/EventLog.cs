using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public class EventLog
    {
        public int Id { get; set; }

        public EventLogLevel LogLevel { get; set; }

        public DateTime? Timestamp { get; set; }

        public string Message { get; set; }

        public int UITestRunEventId { get; set; }

        public UITestRunEvent UITestRunEvent { get; set; }
    }
}
