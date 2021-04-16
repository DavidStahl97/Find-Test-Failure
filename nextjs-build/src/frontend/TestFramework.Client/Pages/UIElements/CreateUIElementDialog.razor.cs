using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class CreateUIElementDialog : ComponentBase
    {
        [Parameter]
        public ChangeOrCreateUIElemenDto UIElement { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        private Task CreateUIElement(ChangeOrCreateUIElemenDto uiElement)
            => Api.SendAsync(x => x.PostUIElementAsync(uiElement));
    }
}
