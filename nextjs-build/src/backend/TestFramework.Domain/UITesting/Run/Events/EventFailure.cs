using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run.Events
{
    public enum EventFailure
    {
        NotStarted,
        Success,
        UnexpectedError,
        WebDriverException,
        ElementNotInteractable,
        NoSuchElement,
        ElementNotVisible,
        InvalidSelector,
        ElementClickIntercepted,
        StaleElementReference,
        InvalidElementState,
        UnhandledAlert,
        NoAlertPresent,
        NoSuchFrame,
        NoSuchWindow,
        Timeout
    }
}
