using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class PutWaitEventDto : PutUIEventDto
    {
        public TimeSpan Ticks { get; set; }        

        protected override string CreatTypeDiscriminator() => nameof(PutWaitEventDto);
    }
}
