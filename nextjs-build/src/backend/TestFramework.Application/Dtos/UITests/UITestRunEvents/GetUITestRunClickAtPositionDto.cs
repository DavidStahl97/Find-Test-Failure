using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class GetUITestRunClickAtPositionDto : GetUITestRunEventDto
    {
        protected override string CreatTypeDiscriminator() => nameof(GetUITestRunClickAtPositionDto);
    }
}
