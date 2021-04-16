using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.IntegrationTests.UIPages
{
    public class BaseLayouterPage : CreateUIPageBase
    {
        public BaseLayouterPage(ITestFrameworkApi api) : base(api)
        {
        }

        protected override ChangeOrCreateUIPageDto CreatePage()
            => new()
            {
                Name = nameof(BaseLayouterPage)
            };

        public async Task<GetUIElementDto> CounterNavButton() => await CreateUIElement(new ChangeOrCreateUIElemenDto
        {
            Name = nameof(CounterNavButton),
            FindByMethod = ChangeOrCreateUIElemenDtoFindByMethod.ByXPath,
            FindBy = @"//*[@id='app']/div/div[1]/div[2]/ul/li[2]/a"
        });

        public static async Task<BaseLayouterPage> Register(ITestFrameworkApi api)
            => await Register(new BaseLayouterPage(api), api);
    }
}
