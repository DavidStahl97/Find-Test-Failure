using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.IntegrationTests.UIPages
{
    public class CounterPage : CreateUIPageBase
    {
        public CounterPage(ITestFrameworkApi api) : base(api)
        {
        }

        protected override ChangeOrCreateUIPageDto CreatePage()
            => new()
            {
                Name = nameof(CounterPage)
            };

        public async Task<GetUIElementDto> CountButton() => await CreateUIElement(new ChangeOrCreateUIElemenDto
        {
            Name = nameof(CountButton),
            FindByMethod = ChangeOrCreateUIElemenDtoFindByMethod.ById,
            FindBy = @"count-button"
        });

        public static async Task<CounterPage> Register(ITestFrameworkApi api)
            => await Register(new CounterPage(api), api);
    }
}
