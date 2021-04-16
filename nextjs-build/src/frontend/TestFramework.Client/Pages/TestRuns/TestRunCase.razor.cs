using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestRuns
{
    public partial class TestRunCase : ComponentBase
    {
        private GetUITestRunCaseDetailsDto testCase;

        [Parameter]
        public int TestRunCaseId { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        protected override async Task OnInitializedAsync()
        {
            testCase = await Api.SendAsync(x => x.GetUITestRunCaseDetailsAsync(TestRunCaseId));            
        }

        private IEnumerable<GetUITestRunEventDto> Events
            => testCase.Events.OrderBy(x => x.Step).ToList();
    }
}
