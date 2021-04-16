using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;
using TestFramework.IntegrationTests.UIPages;
using TestFramework.Utils;
using Xunit;

namespace TestFramework.IntegrationTests
{
    [Collection(nameof(IntegrationTesttCollection))]
    public abstract class UITestRunBase : IAsyncLifetime
    {
        private readonly IntegrationTestFixture _fixture;
        private readonly ITestFrameworkApi _api;

        protected UITestRunBase(IntegrationTestFixture configurationFixture)
        {
            _fixture = configurationFixture;

            var httpClient = _fixture.HttpClientFactory.CreateClient();
            httpClient.BaseAddress = _fixture.Configuration.TestFrameworkUri;

            _api = new TestFrameworkApi(httpClient);
        }

        protected abstract string UITestCaseName { get; }

        protected abstract Task<IEnumerable<PutUIEventDto>> CreateEvents();

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
            BaseLayouter = await BaseLayouterPage.Register(_api);
            CounterPage = await CounterPage.Register(_api);
        }

        protected BaseLayouterPage BaseLayouter { get; private set; }

        protected CounterPage CounterPage { get; private set; }

        [Fact]
        public async Task RunTestCase()
        {
            var uiTestCase = new ChangeOrCreateUITestCaseDto
            {
                Name = UITestCaseName,
                DefaultWaitForUIElement = TimeSpan.FromMinutes(1),
                RunsPeriodically = false,
                StartUrl = _fixture.Configuration.TestWebAppUri
            };

            var uiTestCaseResponse = await _api.SendAsync(x => x.PostUITestCaseAsync(uiTestCase));

            var events = await CreateEvents();
            events.WithIndex()
                .ToList()
                .ForEach(x => x.item.Step = x.index);
            await _api.SendAsync(x => x.PutUITestCaseEventsAsync(uiTestCaseResponse.Id, new UpdateEventsDto
            {
                Events = events.ToList()
            }));

            var startRunResponse = await _api.SendAsync(x => x.PostTestRunAsync(new PostUITestRunDto
            {
                SelectedTestCases = new List<int>
                {
                    uiTestCaseResponse.Id
                }
            }));

            await CheckRun(startRunResponse.Id, TimeSpan.FromMinutes(5));
        }

        private async Task CheckRun(int testRunId, TimeSpan maxTimeSpan)
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();

            var testRun = await _api.SendAsync(x => x.GetUITestRunCasesAsync(testRunId));
            
            if (testRun.TestCases.All(x => x.State == GetUITestRunCaseDtoState.Completed))
            {
                return;
            }

            testRun.TestCases.Should().NotContain(x => x.State == GetUITestRunCaseDtoState.Failure);
            
            stopWatch.Stop();
            var usedTime = stopWatch.Elapsed;

            var residual = maxTimeSpan - usedTime;
            residual.Should().BePositive();

            await Task.Delay(TimeSpan.FromSeconds(1));
            await CheckRun(testRunId, residual);
        }
    }
}
