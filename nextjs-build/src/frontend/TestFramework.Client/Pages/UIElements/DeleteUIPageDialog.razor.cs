using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class DeleteUIPageDialog : ComponentBase
    {
        [Parameter]
        public GetUIPageDto UIPage { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        public async Task Delete()
        {
            await Api.SendAsync(x => x.DeleteUIPageAsync(UIPage.Id));
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}
