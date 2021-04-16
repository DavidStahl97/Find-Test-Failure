using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetClearContentEventDto : GetUIElementEventDto
    {
        protected override string CreatTypeDiscriminator() => nameof(GetClearContentEventDto);
    }
}
