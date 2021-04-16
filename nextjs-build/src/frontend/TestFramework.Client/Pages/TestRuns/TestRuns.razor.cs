using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestRuns
{
    public partial class TestRuns : ComponentBase
    {
        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private async Task<SearchResult<GetUITestRunDto>> Search(SearchQuery query)
        {
            var page = await Api.SendAsync(x => x.GetUITestRunDtoAsync(query.PageIndex, query.PageSize));
            return new SearchResult<GetUITestRunDto>
            {
                Items = page.Data,
                Total = page.Count
            };
        }

        private void TestRunClicked(GetUITestRunDto testRun)
            => Navigation.NavigateTo($"testruns/{testRun.Id}");

        private static List<(int count, string color)> CreateColoredList(GetUITestRunDto run)
            => new()
            {
                (run.NotStartedCount, AppColors.NotStarted),
                (run.StartedCount, AppColors.Started),
                (run.FailedCount, AppColors.Failure),
                (run.CompletedCount, AppColors.Success),
            };
    }
}
