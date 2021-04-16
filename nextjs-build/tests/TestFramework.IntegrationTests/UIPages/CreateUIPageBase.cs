using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.IntegrationTests.UIPages
{
    public abstract class CreateUIPageBase
    {
        private readonly ITestFrameworkApi _api;
        private readonly Dictionary<string, GetUIElementDto> _uiElements = new();

        protected CreateUIPageBase(ITestFrameworkApi api)
        {
            _api = api;
        }

        protected abstract ChangeOrCreateUIPageDto CreatePage();

        public GetUIPageDto Page { get; private set; }

        protected async Task<GetUIElementDto> CreateUIElement(ChangeOrCreateUIElemenDto createUIElement)
        {
            if (_uiElements.TryGetValue(createUIElement.Name, out var uiElement))
            {
                return uiElement;
            }
            else
            {
                createUIElement.PageId = Page.Id;
                var postResponse = await _api.SendAsync(x => x.PostUIElementAsync(createUIElement));
                uiElement = await _api.SendAsync(x => x.GetUIElementByIdAsync(postResponse.Id));
                _uiElements.Add(uiElement.Name, uiElement);
                return uiElement;
            }

        }

        public static async Task<T> Register<T>(T createPage, ITestFrameworkApi api)
            where T : CreateUIPageBase
        {
            var postPage = createPage.CreatePage();
            var pageResponse = await api.SendAsync(x => x.PostUIPageAsync(postPage));
            var getPage = await api.SendAsync(x => x.GetUIPageByIdAsync(pageResponse.Id));

            createPage.Page = getPage;
            return createPage;
        }
    }
}
