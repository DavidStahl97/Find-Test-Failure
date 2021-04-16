using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class EditUIPageDialog : ComponentBase
    {
        [Parameter]
        public int UIPageId { get; set; }

        [Parameter]
        public ChangeOrCreateUIPageDto UIPage { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        public Task EditUIPage(ChangeOrCreateUIPageDto uiPage)
            => Api.SendAsync(x => x.PutUIPageAsync(UIPageId, uiPage));
    }
}
