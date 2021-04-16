using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public class SelectedUIElementData
    {
        public GetAllPageDto Page { get; set; }

        public GetAllUIElementDto UIElement { get; set; }
    }
}
