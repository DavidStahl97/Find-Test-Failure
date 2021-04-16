using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.UITests;
using TestFramework.Domain.UITesting.Run;

namespace TestFramework.Infrastructure.UITests
{
    public class WebUITestFactory : IWebUITestFactory
    {
        private readonly WebUITestingOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public WebUITestFactory(WebUITestingOptions options, IServiceProvider serviceProvider)
        {
            _options = options;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(Browser browser, Func<IWebUITesting, Task> func)
            => await Task.Run(async () => await Start(browser, func));

        private async Task Start(Browser browser, Func<IWebUITesting, Task> func)
        {
            var options = GetDriverOptions(browser);

            var driver = new RemoteWebDriver(new Uri(_options.SeleniumHubUrl),
                options.ToCapabilities(), TimeSpan.FromMinutes(1))
            {
                FileDetector = new LocalFileDetector()
            };

            try
            {
                var remoteDriverApi = _serviceProvider.GetRequiredService<IRemoteWebDriverApi>();
                var logger = _serviceProvider.GetRequiredService<ILogger<WebUITesting>>();

                var webTesting = new WebUITesting(driver, logger, remoteDriverApi);
                await func(webTesting);
            }
            finally
            {
                driver.Close();
                driver.Quit();
                driver.Dispose();
            }
        }

        private static DriverOptions GetDriverOptions(Browser browser)
        {
            return browser switch
            {
                Browser.Chrome => new ChromeOptions(),
                Browser.Edge => new EdgeOptions(),
                Browser.Firefox => new FirefoxOptions(),
                _ => throw new ArgumentException($"Browser not supported {browser}")
            };
        }
    }
}
