using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;
using TestFramework.IntegrationTests.UIPages;
using Xunit;

namespace TestFramework.IntegrationTests
{
    [Collection(nameof(IntegrationTesttCollection))]
    public class SuccessfulTestRun : UITestRunBase
    {
        public SuccessfulTestRun(IntegrationTestFixture integrationTestFixture) : base(integrationTestFixture)
        {
        }

        protected override string UITestCaseName => nameof(SuccessfulTestRun);

        protected override async Task<IEnumerable<PutUIEventDto>> CreateEvents() =>
            new List<PutUIEventDto>
            {
                new PutClickEventDto
                {
                    Name = "Click Counter Nav",
                    UIElementId = (await BaseLayouter.CounterNavButton()).Id,
                    UseDefaultWaitForUIElement = true,
                },
                new PutClickEventDto
                {
                    Name = "Click Count",
                    UIElementId = (await CounterPage.CountButton()).Id,
                    WaitForUIElement = TimeSpan.FromSeconds(1)
                },
                new PutClickEventDto
                {
                    Name = "Click Count",
                    UIElementId = (await CounterPage.CountButton()).Id,
                    WaitForUIElement = TimeSpan.FromSeconds(1)
                }
            };
    }
}
