using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFramework.Client.Components
{
    public partial class Pagination<TItem> : ComponentBase
    {
        private string lastSearch = string.Empty;
        private string search = string.Empty;
        private bool showSkeleton = true;

        private PaginationInfo pagination = PaginationInfo.Default;

        private IEnumerable<TItem> items;

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment<IEnumerable<TItem>> Content { get; set; }

        [Parameter]
        public RenderFragment SkeletonContent { get; set; }

        [Parameter]
        public Func<SearchQuery, Task<SearchResult<TItem>>> SearchFunction { get; set; }

        [Parameter]
        public bool ShowSearch { get; set; } = true;

        protected override Task OnInitializedAsync()
        {
            return SearchAsync();
        }

        public async Task SearchAsync()
        {
            showSkeleton = true;

            var query = CreateSearchQuery();
            var searchTask = SearchFunction(query);

            var delayTask = Task.Delay(500);

            await Task.WhenAll(searchTask, delayTask);

            var result = searchTask.Result;
            SetPage(query, result);

            showSkeleton = false;
        }

        private async Task OnTick()
        {
            if (IsSearching == false)
            {
                return;
            }

            lastSearch = search;

            var query = CreateSearchQuery() with { PageIndex = 0 };
            var result = await SearchFunction(query);

            if (IsSearching)
            {
                return;
            }

            SetPage(query, result);
            showSkeleton = false;
        }

        private void OnSearchKeyUp()
        {
            if (IsSearching)
            {
                showSkeleton = true;
            }
        }

        private bool IsSearching => lastSearch != search;

        private SearchQuery CreateSearchQuery()
        {
            return new SearchQuery
            {
                PageIndex = pagination.PageIndex,
                PageSize = pagination.PageSize,
                SearchString = search
            };
        }

        private void SetPage(SearchQuery query, SearchResult<TItem> result)
        {
            pagination = new PaginationInfo
            {
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Total = result.Total
            };
            items = result.Items;
        }
    }
}
