using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetMoveByOffsetEventDto : GetUIEventDto
    {
        public int OffsetX { get; set; }

        public int OffsetY { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(GetMoveByOffsetEventDto);
    }
}
