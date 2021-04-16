using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetUIEventDto : TypeDiscriminatorDto
    {
        public string Name { get; set; }

        public int Step { get; set; }

        protected override string CreatTypeDiscriminator() => nameof(PutUIEventDto);
    }
}
