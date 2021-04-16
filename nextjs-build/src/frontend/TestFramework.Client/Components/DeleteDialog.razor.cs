using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Components
{
    public partial class DeleteDialog : ComponentBase
    {
        private bool _deletionFailed = false;

        [Parameter]
        public string ResourceType { get; set; }

        [Parameter]
        public string ResourceName { get; set; }
        
        [Parameter]
        public Func<swaggerClient, Task> DeleteAsync { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        private async Task Delete()
        {
            _deletionFailed = false;

            var successful = await Api.SendAsync(DeleteAsync,
                (ErrorCodes.DeletionFailed, () => _deletionFailed = true));

            if (successful)
            {
                MudDialog.Close(DialogResult.Ok(true));
            }
        }
    }
}
