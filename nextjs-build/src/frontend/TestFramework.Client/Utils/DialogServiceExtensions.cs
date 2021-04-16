using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Utils
{
    public static class DialogServiceExtensions
    {
        public static IDialogReference ShowDeleteDialog(this IDialogService dialogService, 
            string entityTypeString, string entityNameString, Func<swaggerClient, Task> sendFunc)
        {
            var parameters = new DialogParameters
            {
                { nameof(DeleteDialog.ResourceType), entityTypeString },
                { nameof(DeleteDialog.ResourceName), entityNameString },
                { nameof(DeleteDialog.DeleteAsync), sendFunc }
            };

            return dialogService.Show<DeleteDialog>(string.Empty, parameters);
        }
    }
}
