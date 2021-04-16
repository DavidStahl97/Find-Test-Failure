using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.BackgroundTasks.UITests
{
    public class EventResult
    {
        public EventFailure Failure { get; init; }

        public string FailureMessage { get; init; }

        public IEnumerable<EventLog> Logs { get; init; }
    }
}
