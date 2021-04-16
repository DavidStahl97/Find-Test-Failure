using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestRunEvents
{
    public class GetUITestRunImportFileEventDto : GetUITestRunUIElementEventDto
    {
        public string FileName { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(GetUITestRunImportFileEventDto);
    }
}
