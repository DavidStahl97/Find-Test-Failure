using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestRuns
{
    public partial class TestRun : ComponentBase
    {
        private GetUITestRunCasesDto testRun;

        [Parameter]
        public int TestRunId { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            testRun = await Api.SendAsync(x => x.GetUITestRunCasesAsync(TestRunId));
        }
    }
}
