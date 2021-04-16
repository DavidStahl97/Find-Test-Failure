using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetImportFileEventDto : GetUIElementEventDto
    {
        public GetUserFileDto UserFile { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(GetImportFileEventDto);
    }
}
