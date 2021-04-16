using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Components
{
    public partial class AsyncButton : ComponentBase
    {
        private bool _inProcess;

        [Parameter]
        public EventCallback<MouseEventArgs> OnAsyncClick { get; set; }

        [Parameter]
        public Color Color { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Class { get; set; }

        private async Task OnClick(MouseEventArgs args)
        {
            try
            {
                _inProcess = true;
                await OnAsyncClick.InvokeAsync(args);
            }
            finally
            {
                _inProcess = false;
                StateHasChanged();
            }
        } 
    }
}
