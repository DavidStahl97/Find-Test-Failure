using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.HealthChecks
{
    public partial class EditHealthCheckDialog : ComponentBase
    {
        [Parameter]
        public int HealthCheckId { get; set; }

        [Parameter]
        public ChangeOrCreateHealthCheckDto HealthCheck { get; set; }

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        public Task Edit(ChangeOrCreateHealthCheckDto healthCheck)
            => Api.SendAsync(x => x.PutHealthCheckAsync(HealthCheckId, healthCheck));
    }
}
