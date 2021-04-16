using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Components
{
    public partial class TablePagination<TItem> : ComponentBase
    {
        private Pagination<TItem> _pagination;

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public Func<SearchQuery, Task<SearchResult<TItem>>> SearchFunction { get; set; }

        [Parameter]
        public EventCallback<TItem> SelectedItemChanged { get; set; }

        [Parameter]
        public RenderFragment ColumnGroup { get; set; }

        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment SkeletonRow { get; set; }

        [Parameter]
        public RenderFragment<TItem> RowTemplate { get; set; }

        [Parameter]
        public bool MultiSelection { get; set; }

        [Parameter]
        public EventCallback<HashSet<TItem>> SelectedItemsChanged { get; set; }

        [Parameter]
        public bool ShowSearch { get; set; }

        public Task SearchAsync()
            => _pagination.SearchAsync();
    }
}
