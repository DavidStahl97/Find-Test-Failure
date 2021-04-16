using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class UIPages : ComponentBase
    {
        private TablePagination<GetUIPageDto> _pagination;

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        private async Task<SearchResult<GetUIPageDto>> Search(SearchQuery query)
        {
            var page = await Api.SendAsync(x => x.GetUIPageAsync(query.PageIndex, query.PageSize, query.SearchString));
            return new SearchResult<GetUIPageDto>
            {
                Items = page.Data,
                Total = page.Count
            };
        }

        private async Task AddUIPage()
        {
            await DialogService.Show<CreateUIPageDialog>().Result;
            await _pagination.SearchAsync();
        }

        private async Task EditUIPage(GetUIPageDto page)
        {
            var changes = new ChangeOrCreateUIPageDto
            {
                Name = page.Name
            };

            var parameters = new DialogParameters
            {
                { nameof(EditUIPageDialog.UIPageId), page.Id },
                { nameof(EditUIPageDialog.UIPage), changes }
            };

            await DialogService.Show<EditUIPageDialog>(string.Empty, parameters).Result;
            await _pagination.SearchAsync();
        }

        private async Task DeleteUIPage(GetUIPageDto page)
        {
            Task deleteFunc(swaggerClient x) => x.DeleteUIPageAsync(page.Id);

            await DialogService.ShowDeleteDialog("UI Page", page.Name, deleteFunc).Result;
            await _pagination.SearchAsync();
        }
    }
}
