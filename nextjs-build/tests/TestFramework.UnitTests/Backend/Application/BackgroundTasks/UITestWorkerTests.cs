using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.UITests;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using Xunit;

namespace TestFramework.UnitTests.Backend.Application.BackgroundTasks
{
    public class UITestWorkerTests
    {
        [Fact]
        public void CreateFindBy_ShouldReplaceWriteInput()
        {
            // Arrange
            var normalUIElement = new UITestRunUIElement
            {
                Name = "normal ui element",
                FindBy = "//*[contains(text(), 'My Button')]"
            };

            var withReplacementOne = new UITestRunUIElement
            {
                FindBy = "//*[contains(text(), '1234 %{1} 56 %{5}')]"
            };

            var withReplacementTwo = new UITestRunUIElement
            {
                FindBy = "//*[contains(text(), 'two %{5} 12 %{1}')]"
            };

            var events = new List<UITestRunEvent>
            {
                new UITestRunWriteEvent
                {
                    Step = 1,
                    Input = "Input1",
                    UIElement = normalUIElement,
                },
                new UITestRunClickAtPositionEvent
                {
                    Step = 2
                },
                new UITestRunClickEvent
                {
                    Step = 3,
                    UIElement = withReplacementOne
                },
                new UITestRunWriteEvent
                {
                    Step = 5,
                    Input = "Input3",
                    UIElement = withReplacementTwo
                }
            };

            // Act
            var firstFindBy = UITestWorker.CreateFindBy(events[0] as UITestRunUIElementEvent, events);
            var secondFindBy = UITestWorker.CreateFindBy(events[2] as UITestRunUIElementEvent, events);
            var thirdFindBy = UITestWorker.CreateFindBy(events[3] as UITestRunUIElementEvent, events);

            // Assert
            firstFindBy.Should().Be("//*[contains(text(), 'My Button')]");
            secondFindBy.Should().Be("//*[contains(text(), '1234 Input1 56 Input3')]");
            thirdFindBy.Should().Be("//*[contains(text(), 'two Input3 12 Input1')]");
        }
    }
}
