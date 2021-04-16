using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class GetUITestRunWaitEventDto : GetUITestRunEventDto
    {
        public TimeSpan Ticks { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(GetUITestRunWaitEventDto);
    }
}
