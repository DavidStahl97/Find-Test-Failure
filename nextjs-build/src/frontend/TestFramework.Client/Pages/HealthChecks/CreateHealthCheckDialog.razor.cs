using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.HealthChecks
{
    public partial class CreateHealthCheckDialog : ComponentBase
    {
        [Inject]
        public ITestFrameworkApi Api { get; set; }

        private Task Create(ChangeOrCreateHealthCheckDto healthCheck)
            => Api.SendAsync(x => x.PostHealthCheckAsync(healthCheck));
    }
}
