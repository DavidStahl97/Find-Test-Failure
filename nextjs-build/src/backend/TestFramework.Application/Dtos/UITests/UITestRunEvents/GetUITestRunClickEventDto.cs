using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class GetUITestRunClickEventDto : GetUITestRunUIElementEventDto
    {
        protected override string CreatTypeDiscriminator() => nameof(GetUITestRunClickEventDto);
    }
}
