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
    public partial class UIElements : ComponentBase
    {
        private TablePagination<GetUIElementDto> _pagination;
        private GetUIPageDetailsDto _details;

        [Parameter]
        public int PageId { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        [Inject]
        public IDialogService DialogService { get; set; }

        private async Task<SearchResult<GetUIElementDto>> Search(SearchQuery query)
        {
            _details = await Api.SendAsync(x => x.GetUIPageDetailsAsync(PageId, 
                query.PageIndex, query.PageSize, query.SearchString));
            return new SearchResult<GetUIElementDto>
            {
                Items = _details.Page.Data,
                Total = _details.Page.Count
            };
        }

        private async Task AddUIElement()
        {
            var create = new ChangeOrCreateUIElemenDto
            {
                PageId = PageId,
            };

            var parameter = new DialogParameters
            {
                { nameof(CreateUIElementDialog.UIElement), create }
            };

            await DialogService.Show<CreateUIElementDialog>(string.Empty, parameter).Result;
            await _pagination.SearchAsync();
        }

        private async Task EditUIElement(GetUIElementDto uiElement)
        {
            var changes = new ChangeOrCreateUIElemenDto
            {
                FindBy = uiElement.FindBy,
                FindByMethod = Enum.Parse<ChangeOrCreateUIElemenDtoFindByMethod>(uiElement.FindByMethod.ToString()),
                Name = uiElement.Name,
                PageId = PageId
            };

            var parameters = new DialogParameters
            {
                { nameof(EditUIElementDialog.UIElementId), uiElement.Id },
                { nameof(EditUIElementDialog.UIElement), changes }
            };
            
            await DialogService.Show<EditUIElementDialog>(string.Empty, parameters).Result;
            await _pagination.SearchAsync();
        }

        private async Task DeleteUIElement(GetUIElementDto uiElement)
        {
            Task func(swaggerClient x) => x.DeleteUIElementAsync(uiElement.Id);

            await DialogService.ShowDeleteDialog("UI Element", uiElement.Name, func).Result;
            await _pagination.SearchAsync();
        }
    }
}
