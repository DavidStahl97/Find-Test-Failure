using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Domain.UITesting.Run
{
    public class UITestRunCase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri StartUrl { get; set; }

        public DateTime? Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public Browser Browser { get; set; }

        public UITestCaseState State { get; set; } = UITestCaseState.NotStarted;

        public TimeSpan DefaultWaitForUIElement { get; set; }

        public bool AutomaticallyStarted { get; set; }

        public UITestRunCaseFailureSendedState FailureSendedState { get; set; }

        public virtual ICollection<UITestRunEvent> Events { get; set; }

        public int UITestRunId { get; set; }

        public virtual UITestRun TestRun { get; set; }

        public void UpdateState(UITestRunCase testCase)
        {
            Start = testCase.Start;
            Duration = testCase.Duration;
            State = testCase.State;
        }
    }
}
