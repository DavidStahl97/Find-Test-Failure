using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class CreateTestCaseDialog : ComponentBase
    {
        [Inject]
        public ITestFrameworkApi Api { get; set; }

        public Task Create(ChangeOrCreateUITestCaseDto testCase)
            => Api.SendAsync(x => x.PostUITestCaseAsync(testCase));
    }
}
