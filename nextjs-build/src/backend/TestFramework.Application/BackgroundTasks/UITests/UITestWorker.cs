using Microsoft.Extensions.Logging;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using TestFramework.Application.Handler.UITests.UITestRun;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Utils;

namespace TestFramework.Application.UITests
{
    public class UITestWorker : IBackgroundWorker<UITestRunCase>
    {
        private readonly ILogger<UITestWorker> _logger;
        private readonly IWebUITestFactory _factory;
        private readonly IParallelPublisher _publisher;
        private readonly string _filesFolder;

        public UITestWorker(IWebUITestFactory factory, IParallelPublisher publisher,
            IFileStorage fileStorage, ILogger<UITestWorker> logger)
        {
            _factory = factory;
            _publisher = publisher;
            _filesFolder = fileStorage.Folder;
            _logger = logger;
        }

        public async Task ExecuteTaskAsync(UITestRunCase testCase, CancellationToken token)
        {
            await _factory.StartAsync(testCase.Browser, async testing =>
            {
                try
                {
                    await ExecuteMeasureSteps(testing, testCase);
                }
                catch (Exception ex)
                {
                    _logger.WriteFatal(ex, "Critical TestCase Run failed",
                        ("testCaseId", testCase.Id));

                    testCase.State = UITestCaseState.Failure;
                    await SendFailure(testCase);

                    throw;
                }
            });
        }

        private async Task ExecuteMeasureSteps(IWebUITesting testing, UITestRunCase testCase)
        {
            testCase.Start = DateTime.UtcNow;
            testCase.State = UITestCaseState.Started;

            await _publisher.Publish(new UpdateStartTestCaseRequest { TestCase = testCase });

            var (timeSpan, wasSuccessful) = await Metrics.MeasureAsync(() => ExecuteSteps(testing, testCase));

            testCase.Duration = timeSpan;

            await wasSuccessful.Match(
                async t =>
                {
                    testCase.State = UITestCaseState.Completed;
                    await SendTestCaseSuccess(testCase);
                },
                async f =>
                {
                    testCase.State = UITestCaseState.Failure;
                    await SendFailure(testCase);
                });
        }

        private async Task<TrueOrFalse> ExecuteSteps(IWebUITesting testing, UITestRunCase testCase)
        {
            // To-Do: Generate Event
            var navResult = testing.NavigateTo(testCase.StartUrl);

            return await ExecuteEvents(testing, testCase);
        }

        private async Task<TrueOrFalse> ExecuteEvents(IWebUITesting testing, UITestRunCase testCase)
        {
            foreach (var uiEvent in testCase.Events.OrderBy(x => x.Step))
            {
                var success = await TryExecuteEvent(uiEvent, testing, testCase);
                if (success == false)
                {
                    testCase.Events
                        .Where(x => x.Step > uiEvent.Step)
                        .ToList()
                        .ForEach(x => x.State = UITestEventState.Cancel);
                    return false;
                }
            }

            return true;
        }

        private async Task<bool> TryExecuteEvent(UITestRunEvent uiEvent, IWebUITesting testing, UITestRunCase testCase)
        {
            uiEvent.Start = DateTime.UtcNow;

            var (timeSpan, result) = Metrics.Measure(() => ExecuteEvent(testing, uiEvent, testCase.Events));

            uiEvent.Result = result;
            uiEvent.Duration = timeSpan;

            var logs = await testing.GetLogsAsync();
            uiEvent.Logs = logs.ToList();

            if (uiEvent.Result == EventFailure.Success)
            {
                uiEvent.State = UITestEventState.Completed;
                await SendSuccessEventResult(uiEvent);
                return true;
            }
            else
            {
                uiEvent.State = UITestEventState.Failure;
                return false;
            }
        }

        private Task SendFailure(UITestRunCase testCase)
        {
            return _publisher.Publish(new FailureTestCaseRequest { TestCase = testCase });
        }

        private Task SendTestCaseSuccess(UITestRunCase testCase)
        {            
            return _publisher.Publish(new UpdateFinishedTestCaseRequest { TestCase = testCase });
        }

        private Task SendSuccessEventResult(UITestRunEvent uiEvent)
            => _publisher.Publish(new UpdateEventRequest { Event = uiEvent });

        private EventFailure ExecuteEvent(IWebUITesting testing, 
            UITestRunEvent uiEvent, ICollection<UITestRunEvent> events) 
        {
            return uiEvent switch
            {
                UITestRunClickEvent clickEvent => testing.Click(clickEvent.UIElement.FindByMethod, 
                    CreateFindBy(clickEvent, events),
                    SelectTimeSpan(clickEvent)),

                UITestRunWaitEvent waitEvent => ExecuteWaitEvent(waitEvent),

                UITestRunWriteEvent writeEvent => testing.WriteEvent(
                    writeEvent.UIElement.FindByMethod, CreateFindBy(writeEvent, events),
                    SelectTimeSpan(writeEvent),
                    writeEvent.Input),

                UITestRunMoveToUIElementEvent moveToEvent => testing.MoveToUIElement(
                    moveToEvent.UIElement.FindByMethod, CreateFindBy(moveToEvent, events),
                    SelectTimeSpan(moveToEvent)),

                UITestRunClickAtPositionEvent => testing.ClickAtPosition(),

                UITestRunMoveByOffsetEvent moveByOffsetEvent => testing.MoveByOffset(
                    moveByOffsetEvent.OffsetX, moveByOffsetEvent.OffsetY),

                UITestRunClearContentEvent clearContentEvent => testing.ClearContent(
                    clearContentEvent.UIElement.FindByMethod, CreateFindBy(clearContentEvent, events),
                    SelectTimeSpan(clearContentEvent)),

                UITestRunImportFileEvent importFileEvent => ExecuteImportFileEvent(importFileEvent, testing),

                _ => throw new ArgumentException("Invalid Test Event")
            };
        }

        private EventFailure ExecuteImportFileEvent(UITestRunImportFileEvent importFileEvent, IWebUITesting testing)
        {
            var sendKeys = Path.Combine(_filesFolder, importFileEvent.StoredFileName);
            return testing.WriteEvent(
                importFileEvent.UIElement.FindByMethod,
                importFileEvent.UIElement.FindBy,
                SelectTimeSpan(importFileEvent),
                sendKeys);
        }

        private static EventFailure ExecuteWaitEvent(UITestRunWaitEvent waitEvent)
        {
            Thread.Sleep(waitEvent.Ticks);
            return EventFailure.Success;
        }

        private static TimeSpan SelectTimeSpan(UITestRunUIElementEvent uiElementEvent)
            => uiElementEvent.UseDefaultWaitForUIElement ?
                uiElementEvent.RunCase.DefaultWaitForUIElement : uiElementEvent.WaitForUIElement;

        public static string CreateFindBy(UITestRunUIElementEvent uiElementEvent, 
            ICollection<UITestRunEvent> events)
        {
            var findByWithReplacements = uiElementEvent.UIElement.FindBy;

            var replaceMatches = Regex.Matches(uiElementEvent.UIElement.FindBy, @"%{\d+}");
            foreach (var match in replaceMatches.AsEnumerable())
            {
                var replaceString = match.Value[2..^1];
                if(int.TryParse(replaceString, out int eventStep))
                {
                    var uiEvent = events.FirstOrDefault(x => x.Step == eventStep);
                    if (uiEvent is not null && uiEvent is UITestRunWriteEvent writeEvent)
                    {
                        findByWithReplacements = findByWithReplacements.Replace(match.Value, writeEvent.Input);
                    }
                }
            }

            return findByWithReplacements;
        }
    }
}
