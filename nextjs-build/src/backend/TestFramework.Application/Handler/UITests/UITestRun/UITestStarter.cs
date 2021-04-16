using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.BackgroundTasks;
using TestFramework.Application.Repository;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Domain.UITesting.Run.Events;
using TestFramework.Domain.UITesting.Template;
using TestFramework.Domain.UITesting.Template.Events;

namespace TestFramework.Application.Handler.UITests.UITestRun
{
    public class UITestStarter : IUITestStarter
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBackgroundTaskQueue<UITestRunCase> _queue;

        public UITestStarter(IRepository repository, IMapper mapper, IBackgroundTaskQueue<UITestRunCase> queue)
        {
            _repository = repository;
            _mapper = mapper;
            _queue = queue;
        }

        public async Task<int> ExecuteAsync(IEnumerable<int> selectedTestCases)
        {
            var testCases = await _repository.UITestCases.GetByIdsWithEvents(selectedTestCases);

            await AddUserFiles(testCases);

            var runTestCases = _mapper.Map<IEnumerable<UITestRunCase>>(testCases);

            (var uiElements, var uiElementsMap) = await StoreTestRunUIElements(testCases);

            var testRunCases = CreateTestRunCases(testCases, uiElementsMap);
            GenerateUniqueInputs(testRunCases);
            var run = new Domain.UITesting.Run.UITestRun
            {
                Start = DateTime.UtcNow,
                TestCases = testRunCases
            };

            _repository.UITestRuns.Add(run);
            await _repository.SaveChangesAsync();

            UpdateUIElements(testRunCases, uiElements);

            run.TestCases.ToList()
                .ForEach(x => _queue.EnqueueBackgroundTask(x));

            return run.Id;
        }

        private ICollection<UITestRunCase> CreateTestRunCases(IEnumerable<UITestCase> testCases,
            Func<int, UITestRunUIElement> uiElementMap)
        {
            return new[] { Browser.Chrome, Browser.Firefox, Browser.Edge }
                .Select(browser => CreateTestCasesPerBrowser(testCases, uiElementMap, browser))
                .SelectMany(x => x)
                .ToList();
        }

        private ICollection<UITestRunCase> CreateTestCasesPerBrowser(IEnumerable<UITestCase> testCases,
            Func<int, UITestRunUIElement> uiElementMap, Browser browser)
        {
            var runTestCases = _mapper.Map<ICollection<UITestRunCase>>(testCases);
            UpdateUIElementId(runTestCases, uiElementMap);

            foreach (var testCase in runTestCases)
            {
                testCase.Browser = browser;
            }

            return runTestCases;
        }

        private static void UpdateUIElements(IEnumerable<UITestRunCase> testCases, IEnumerable<UITestRunUIElement> uiElements)
        {
            testCases.SelectMany(x => x.Events)
                .Where(x => x is UITestRunUIElementEvent)
                .Select(x => x as UITestRunUIElementEvent)
                .ToList()
                .ForEach(x => x.UIElement = uiElements.Single(y => y.Id == x.UITestRunUIElementId));
        }

        private static void UpdateUIElementId(IEnumerable<UITestRunCase> testCases, Func<int, UITestRunUIElement> uiElementMap)
        {
            testCases.SelectMany(x => x.Events)
                .Where(x => x is UITestRunUIElementEvent)
                .Select(x => x as UITestRunUIElementEvent)
                .ToList()
                .ForEach(x =>
                {
                    var newId = uiElementMap(x.UITestRunUIElementId).Id;
                    x.UITestRunUIElementId = newId;
                });
        }

        private async Task<(IEnumerable<UITestRunUIElement>, Func<int, UITestRunUIElement>)> StoreTestRunUIElements(
            IEnumerable<UITestCase> testCases)
        {
            var eventsWithUIElement = testCases.SelectMany(x => x.Events)
                .Where(x => x is UIElementEvent)
                .Select(x => x as UIElementEvent)
                .ToList();

            var distinctElementIds = eventsWithUIElement
                .Select(x => x.UIElementId)
                .Distinct()
                .ToList();

            var uiElements = await _repository.UIElements.GetRangeByIdAsync(distinctElementIds);
            var testRunUIElements = _mapper.Map<IEnumerable<UITestRunUIElement>>(uiElements);

            _repository.UITestRunUIElements.AddRange(testRunUIElements);
            await _repository.SaveChangesAsync();

            var uiElementIdsMap = uiElements.Select((uiElement, i) => new { Id = i, UIElement = uiElement })
                .Join(testRunUIElements.Select((uiElements, i) => new { Id = i, UIElement = uiElements }),
                    x => x.Id,
                    x => x.Id,
                    (x, y) => new { x.UIElement, TestRunUIElement = y.UIElement })
                .ToList();

            return (testRunUIElements, id => uiElementIdsMap.Single(x => x.UIElement.Id == id).TestRunUIElement);
        }

        private static void GenerateUniqueInputs(IEnumerable<UITestRunCase> cases)
        {
            cases.SelectMany(x => x.Events)
                .Where(x => x is UITestRunWriteEvent)
                .Select(x => x as UITestRunWriteEvent)
                .Where(x => x.GenerateUnique)
                .ToList()
                .ForEach(x => x.Input = Guid.NewGuid().ToString());
        }

        private async Task AddUserFiles(IEnumerable<UITestCase> testCases)
        {
            var importFileEvents = testCases
                .SelectMany(x => x.Events)
                .Where(x => x is ImportFileEvent)
                .Select(x => x as ImportFileEvent)
                .ToList();

            var importFileEventIds = importFileEvents
                .Select(x => x.UserFileId)
                .Distinct()
                .ToList();

            var userFiles = await _repository.UserFiles.GetRangeAsync(importFileEventIds);

            importFileEvents.ForEach(x => x.UserFile =
                userFiles.Single(file => file.Id == x.UserFileId));
        }
    }
}
