using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.Home
{
    public partial class HealthChecks : ComponentBase
    {
        private IEnumerable<GetHealthCheckDto> _healthChecks = new List<GetHealthCheckDto>();

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var page = await Api.SendAsync(x => x.GetHealthCheckPageAsync(0, 100, string.Empty));
            _healthChecks = page.Data;
        }
    }
}
