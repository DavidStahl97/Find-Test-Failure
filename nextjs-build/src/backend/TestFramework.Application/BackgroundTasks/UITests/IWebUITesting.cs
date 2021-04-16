using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;

namespace TestFramework.Application.UITests
{
    public interface IWebUITesting
    {
        Task<IEnumerable<EventLog>> GetLogsAsync();

        EventFailure NavigateTo(Uri uri);

        EventFailure Click(FindByMethod method, string findBy, TimeSpan waitForUIElement);

        EventFailure WriteEvent(FindByMethod method, string findBy, TimeSpan waitForUIElement, string input);

        EventFailure MoveToUIElement(FindByMethod method, string findBy, TimeSpan waitForUIElement);

        EventFailure ClickAtPosition();

        EventFailure MoveByOffset(int offsetX, int offsetY);

        EventFailure ClearContent(FindByMethod method, string findBy, TimeSpan waitForUIElement);
    }
}
