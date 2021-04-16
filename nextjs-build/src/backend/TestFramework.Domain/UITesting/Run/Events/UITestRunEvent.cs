using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public class UITestRunEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public UITestEventState State { get; set; } = UITestEventState.NotStarted;

        public EventFailure Result { get; set; } = EventFailure.NotStarted;

        public int Step { get; set; }

        public int UIRunCase { get; set; }

        public UITestRunCase RunCase { get; set; }

        public ICollection<EventLog> Logs { get; set; }

        public void UpdateState(UITestRunEvent uiEvent)
        {
            Start = uiEvent.Start;
            Duration = uiEvent.Duration;
            State = uiEvent.State;
            Result = uiEvent.Result;
            Logs = uiEvent.Logs;
        }

        public const int NameMaxLength = 255;
    }
}
