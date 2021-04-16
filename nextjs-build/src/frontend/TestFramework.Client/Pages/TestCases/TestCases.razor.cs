using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases
{
    public partial class TestCases : ComponentBase
    {
        private bool _startsRun = false;
        private HashSet<GetUITestCaseDto> Selected = new();
        private TablePagination<GetUITestCaseDto> _pagination;

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private bool DisableStartButton => Selected.Any() == false;

        private async Task<SearchResult<GetUITestCaseDto>> Search(SearchQuery query)
        {
            var page = await Api.SendAsync(x => x.GetUITestPageAsync(query.PageIndex, query.PageSize, 
                query.SearchString));
            return new SearchResult<GetUITestCaseDto>
            {
                Items = page.Data,
                Total = page.Count
            };
        }

        private void SelectedTestCases(HashSet<GetUITestCaseDto> selected)
        {
            Selected = selected;
        }

        private Task StartRun()
        {
            var ids = Selected.Select(x => x.Id)
                .ToList();

            var parameters = new DialogParameters
            {
                { nameof(StartRunDialog.SelectedCases), ids }
            };

            return DialogService.Show<StartRunDialog>(string.Empty, parameters).Result;
        }

        private async Task AddTestCase()
        {
            await DialogService.Show<CreateTestCaseDialog>().Result;
            await _pagination.SearchAsync();
        }

        private async Task EditTestCase(GetUITestCaseDto testCase)
        {
            var changes = new ChangeOrCreateUITestCaseDto
            {
                Name = testCase.Name,
                StartUrl = testCase.StartUrl,
                DefaultWaitForUIElement = testCase.DefaultWaitForUIElement,
                RunsPeriodically = testCase.RunsPeriodically
            };

            var parameters = new DialogParameters
            {
                { nameof(EditTestCaseDialog.TestCaseId), testCase.Id },
                { nameof(EditTestCaseDialog.TestCase), changes }
            };

            await DialogService.Show<EditTestCaseDialog>(string.Empty, parameters).Result;
            await _pagination.SearchAsync();
        }

        private async Task DeleteTestCase(GetUITestCaseDto testCase)
        {
            Task deleteFunc(swaggerClient x) => x.DeleteTestCaseAsync(testCase.Id);

            await DialogService.ShowDeleteDialog("UI Test Case", testCase.Name, deleteFunc).Result;
            await _pagination.SearchAsync();
        }
    }
}
