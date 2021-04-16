using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class SavedUIEvents : ComponentBase
    {
        [Parameter]
        public IEnumerable<GetUIEventDto> Events { get; set; }
    }
}
