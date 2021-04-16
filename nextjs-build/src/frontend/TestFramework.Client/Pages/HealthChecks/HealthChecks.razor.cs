using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Components;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.HealthChecks
{
    public partial class HealthChecks : ComponentBase
    {
        private TablePagination<GetHealthCheckDto> _pagination;

        [Inject]
        public IDialogService DialogService { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        private async Task<SearchResult<GetHealthCheckDto>> Search(SearchQuery query)
        {
            var page = await Api.SendAsync(x => x.
                GetHealthCheckPageAsync(query.PageIndex, query.PageSize, query.SearchString));
            return new SearchResult<GetHealthCheckDto>
            {
                Items = page.Data,
                Total = page.Count
            };
        }

        private async Task Create()
        {
            await DialogService.Show<CreateHealthCheckDialog>().Result;
            await _pagination.SearchAsync();
        }

        private async Task Edit(GetHealthCheckDto healthCheck)
        {
            var changes = new ChangeOrCreateHealthCheckDto
            {
                Name = healthCheck.Name,
                Url = healthCheck.Url
            };

            var parameters = new DialogParameters
            {
                { nameof(EditHealthCheckDialog.HealthCheckId), healthCheck.Id },
                { nameof(EditHealthCheckDialog.HealthCheck), changes }
            };

            await DialogService.Show<EditHealthCheckDialog>(string.Empty, parameters).Result;
            await _pagination.SearchAsync();
        }

        private async Task Delete(GetHealthCheckDto healthCheck)
        {
            Task deleteFunc(swaggerClient x) => x.DeleteHealthCheckAsync(healthCheck.Id);

            await DialogService.ShowDeleteDialog("Health Check", healthCheck.Name, deleteFunc).Result;
            await _pagination.SearchAsync();
        }
    }
}
