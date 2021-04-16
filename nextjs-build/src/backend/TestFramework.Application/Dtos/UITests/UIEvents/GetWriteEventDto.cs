using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetWriteEventDto : GetUIElementEventDto
    {
        public string Input { get; set; }

        public bool GenerateUnique { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(GetWriteEventDto);
    }
}
