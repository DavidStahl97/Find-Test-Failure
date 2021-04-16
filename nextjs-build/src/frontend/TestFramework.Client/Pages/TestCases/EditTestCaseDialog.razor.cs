using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class EditTestCaseDialog : ComponentBase
    {
        [Parameter]
        public int TestCaseId { get; set; }

        [Parameter]
        public ChangeOrCreateUITestCaseDto TestCase { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        public Task Edit(ChangeOrCreateUITestCaseDto testCase)
            => Api.SendAsync(x => x.PutUITestCaseAsync(TestCaseId, testCase));
    }
}
