using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application;
using TestFramework.Domain.UITesting.Run;
using TestFramework.Infrastructure.MicrosoftTeams.Dto;

namespace TestFramework.Infrastructure.MicrosoftTeams
{
    public class TestErrorNotifyService : ITestErrorNotifyService
    {
        private readonly IMicrosoftTeamsApi _microsoftTeamsApi;
        private readonly MicrosoftTeamsApiOptions _options;

        public TestErrorNotifyService(IMicrosoftTeamsApi microsoftTeamsApi, MicrosoftTeamsApiOptions options)
        {
            _microsoftTeamsApi = microsoftTeamsApi;
            _options = options;
        }

        public async Task<TrueOrFalse> NotifyAsync(UITestRunCase testCase)
        {
            var linkUri = $"{_options.WebApiBaseUri}/testruns/testruncases/{testCase.Id}";

            var card = new Card
            {
                Summary = "UI Test Error",
                ThemeColor = "0076D7",
                Sections = new List<Section>
                {
                    new Section
                    {
                        ActivityTitle = "UI Test Error",
                        Facts = new List<Fact>
                        {
                            new Fact { Name = "UI Test", Value = testCase.Name },
                            new Fact { Name = "Started", Value = testCase.Start.ToString() ?? string.Empty },
                            new Fact { Name = "Browser", Value = testCase.Browser.ToString() }
                        }
                    }
                },
                PotentialAction = new List<CardAction>
                {
                    new CardAction
                    {
                        Name = "Link",
                        Targets = new List<Target>
                        {
                            new Target { Uri = linkUri }
                        }
                    }
                }
            };

            return await _microsoftTeamsApi.PostMessage(card);
        }
    }
}
