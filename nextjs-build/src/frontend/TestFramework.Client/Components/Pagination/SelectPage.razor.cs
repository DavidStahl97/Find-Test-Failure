using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Components
{
    public partial class SelectPage : ComponentBase
    {
        [CascadingParameter]
        public PaginationInfo Pagination { get; set; }

        [Parameter]
        public EventCallback PageChangedEvent { get; set; }

        private string CurrentSelectedPage
            => $"{FirstElementOfPage}-{LastElementOfPage} of {Pagination.Total}";

        private int LastElementOfPage
            => Pagination.PageIndex == LastPageIndex ? Pagination.Total : Pagination.PageIndex * Pagination.PageSize + Pagination.PageSize;

        private int FirstElementOfPage
            => Pagination.Total == 0 ? 0 : Pagination.PageIndex * Pagination.PageSize + 1;

        private int LastPageIndex => (Pagination.Total - 1) / Pagination.PageSize;

        private async Task OnSelectFirstPage()
        {
            Pagination.PageIndex = 0;
            await PageChangedEvent.InvokeAsync();
        }

        private async Task OnSelectNextPage()
        {
            Pagination.PageIndex++;
            await PageChangedEvent.InvokeAsync();
        }

        private async Task OnSelectBeforePage()
        {
            Pagination.PageIndex--;
            await PageChangedEvent.InvokeAsync();
        }

        private async Task OnSelectLastPage()
        {
            Pagination.PageIndex = LastPageIndex;
            await PageChangedEvent.InvokeAsync();
        }

        private async Task OnPageSizeChanged(int pageSize)
        {
            Pagination.PageIndex = 0;
            Pagination.PageSize = pageSize;
            await PageChangedEvent.InvokeAsync();
        }
    }
}
