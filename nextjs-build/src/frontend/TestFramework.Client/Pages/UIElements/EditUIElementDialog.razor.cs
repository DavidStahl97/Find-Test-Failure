using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class EditUIElementDialog : ComponentBase
    {        
        [Parameter]
        public int UIElementId { get; set; }

        [Parameter]
        public ChangeOrCreateUIElemenDto UIElement { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        public Task EditUIElement(ChangeOrCreateUIElemenDto uiElement)
            => Api.SendAsync(x => x.PutUIElementAsync(UIElementId, uiElement));
    }
}
