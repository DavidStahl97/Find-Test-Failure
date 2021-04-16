using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Template.Events
{
    public class MoveByOffsetEvent : UIEvent
    {
        public int OffsetX { get; set; }

        public int OffsetY { get; set; }
    }
}
