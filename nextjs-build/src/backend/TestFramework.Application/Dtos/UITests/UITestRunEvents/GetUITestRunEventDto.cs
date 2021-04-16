using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class GetUITestRunEventDto : TypeDiscriminatorDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Start { get; set; }

        public TimeSpan? Duration { get; set; }

        public UITestEventState State { get; set; }

        public EventFailure Result { get; set; }

        public int Step { get; set; }

        public IEnumerable<EventLogDto> Logs { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(GetUITestRunEventDto);
    }
}
