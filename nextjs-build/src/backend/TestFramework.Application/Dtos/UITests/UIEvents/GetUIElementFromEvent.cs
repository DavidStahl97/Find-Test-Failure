using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;

namespace TestFramework.Application.Dtos.UITests.UIEvents
{
    public class GetUIElementFromEvent
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public GetUIPageDto Page { get; set; }
    }
}
