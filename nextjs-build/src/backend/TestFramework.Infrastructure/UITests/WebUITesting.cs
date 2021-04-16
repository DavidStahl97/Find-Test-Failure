using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Application.UITests;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Infrastructure.UITests
{
    public class WebUITesting : IWebUITesting
    {
        private readonly RemoteWebDriver _driver;
        private readonly IRemoteWebDriverApi _remoteWebDriverApi;
        private readonly ILogger<WebUITesting> _logger;

        public WebUITesting(RemoteWebDriver driver, ILogger<WebUITesting> logger, IRemoteWebDriverApi remoteWebDriverApi)
        {
            _driver = driver;
            _logger = logger;
            _remoteWebDriverApi = remoteWebDriverApi;
        }

        public async Task<IEnumerable<EventLog>> GetLogsAsync()
            => await _remoteWebDriverApi.GetLogsAsync(_driver.SessionId.ToString());

        public EventFailure Click(FindByMethod method, string findBy, TimeSpan waitForUIElement)
            => HandleFailures(method, findBy, waitForUIElement, x => x.Click());

        public EventFailure NavigateTo(Uri uri)
            => HandleFailures(TimeSpan.FromSeconds(30), () => _driver.Navigate().GoToUrl(uri));

        public EventFailure WriteEvent(FindByMethod method, string findBy, TimeSpan waitForUIElement, string input)
            => HandleFailures(method, findBy, waitForUIElement, x => x.SendKeys(input));

        public EventFailure MoveToUIElement(FindByMethod method, string findBy, TimeSpan waitForUIElement)
            => HandleFailures(method, findBy, waitForUIElement, 
                x => ExecuteAction(action => action.MoveToElement(x)));

        public EventFailure ClickAtPosition()
            => HandleFailures(TimeSpan.FromSeconds(1), () => new Actions(_driver).Click().Perform());

        public EventFailure MoveByOffset(int offsetX, int offsetY)
            => HandleFailures(TimeSpan.FromSeconds(1), 
                () => ExecuteAction(x => x.MoveByOffset(offsetX, offsetY)));

        public EventFailure ClearContent(FindByMethod method, string findBy, TimeSpan waitForUIElement)
            => HandleFailures(method, findBy, waitForUIElement,
                 x => x.Clear());

        private void ExecuteAction(Func<Actions, Actions> execute)
            => execute(new Actions(_driver)).Perform();

        private EventFailure HandleFailures(TimeSpan waitForUIElement, Action action)
        {
            try
            {
                var wait = new WebDriverWait(_driver, waitForUIElement);
                wait.IgnoreExceptionTypes(typeof(Exception));

                wait.Until(driver =>
                {
                    action();
                    return true;
                });

                return EventFailure.Success;
            }
            catch (WebDriverTimeoutException ex)
            {
                var result = ex.InnerException switch
                {
                    null => EventFailure.Timeout,
                    ElementNotVisibleException => EventFailure.ElementNotVisible,
                    ElementClickInterceptedException => EventFailure.ElementClickIntercepted,
                    ElementNotInteractableException => EventFailure.ElementNotInteractable,
                    InvalidSelectorException => EventFailure.InvalidSelector,
                    NoSuchElementException => EventFailure.NoSuchElement,
                    StaleElementReferenceException => EventFailure.StaleElementReference,
                    InvalidElementStateException => EventFailure.InvalidElementState,
                    UnhandledAlertException => EventFailure.UnhandledAlert,
                    NoAlertPresentException => EventFailure.NoAlertPresent,
                    NoSuchFrameException => EventFailure.NoSuchFrame,
                    NoSuchWindowException => EventFailure.NoSuchWindow,
                    WebDriverException => EventFailure.WebDriverException,
                    Exception => EventFailure.UnexpectedError,
                };

                _logger.WriteError(ex, "UI Event Failure");

                return result;
            }
        }

        private EventFailure HandleFailures(FindByMethod method, string findBy, TimeSpan waitForUIElement, Action<IWebElement> func)
        {
            var result = HandleFailures(waitForUIElement, () => func(GetWebElement(_driver, method, findBy)));
            if (result != EventFailure.Success)
            {
                _logger.WriteInformation("UI Event Failure",
                    (nameof(method), method),
                    (nameof(findBy), findBy),
                    (nameof(EventFailure), result));
            }

            return result;
        }

        private static IWebElement GetWebElement(IWebDriver driver, FindByMethod method, string findBy)
            => driver.FindElement(FindBy(method, findBy));

        private static By FindBy(FindByMethod method, string findBy)
        {
            return method switch
            {
                FindByMethod.ById => By.Id(findBy),
                FindByMethod.ByClass => By.ClassName(findBy),
                FindByMethod.ByXPath => By.XPath(findBy),
                _ => throw new ArgumentException("By Method not exists")
            };
        }
    }
}