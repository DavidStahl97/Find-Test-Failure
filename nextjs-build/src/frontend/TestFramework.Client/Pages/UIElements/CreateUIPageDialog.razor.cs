using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.UIElements
{
    public partial class CreateUIPageDialog : ComponentBase
    {
        [Inject]
        public ITestFrameworkApi Api { get; set; }

        private Task CreateUIPage(ChangeOrCreateUIPageDto dto)
            => Api.SendAsync(x => x.PostUIPageAsync(dto));
    }
}
