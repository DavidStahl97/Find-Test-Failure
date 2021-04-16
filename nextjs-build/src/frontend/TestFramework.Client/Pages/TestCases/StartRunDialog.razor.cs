using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class StartRunDialog : ComponentBase
    {
        [Parameter]
        public IEnumerable<int> SelectedCases { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var response = await Api.SendAsync(x => x.PostTestRunAsync(new PostUITestRunDto
            {
                SelectedTestCases = SelectedCases.ToList()
            }));

            MudDialog.Close(DialogResult.Ok(true));

            Navigation.NavigateTo($"/testruns/{response.Id}");
        }
    }
}
