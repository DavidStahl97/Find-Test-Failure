using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.Utils;
using TestFramework.Client.WebAPI;
using TestFramework.Utils;

namespace TestFramework.Client.Pages.Home
{
    public partial class TestCaseRunsDonutChart : ComponentBase
    {
        private DateRange _dateRange = new(DateTime.Now.Date, DateTime.Now.Date);

        private readonly List<TestCasesStatusInfoState> _states = new()
        {
            TestCasesStatusInfoState.NotStarted,
            TestCasesStatusInfoState.Started,
            TestCasesStatusInfoState.Completed,
            TestCasesStatusInfoState.Failure
        };

        private string[] _labels;

        private double[] _data;

        private ChartOptions _chartOptions = new()
        {
            DisableLegend = false,
            ChartPalette = new string[]
            {
                AppColors.NotStarted, AppColors.Started, AppColors.Success, AppColors.Failure
            }
        };

        [Inject]
        public ITestFrameworkApi Api { get; set; }

        protected override Task OnInitializedAsync()
        {
            _data = _states.Select(x => 0d).ToArray();
            _labels = _states.Select(x => Enum.GetName(x)).ToArray();

            return UpdateStates(_dateRange);
        }

        private void UpdateDataItem(int index, TestCasesStatusInfo info)
        {
            if (info is null)
            {
                _data[index] = 0;
            }
            else
            {
                _data[index] = info.Count;
                _labels[index] = $"{info.State}: {info.Count}";
            }
        }

        private async Task UpdateStates(DateRange range)
        {
            _dateRange = range;
            var end = range.End.HasValue ? range.End.Value.AddDays(1) : range.Start.Value.AddDays(1);
            var stateCounts = await Api.SendAsync(x => x
                .GetTestCasesStatusInfosAsync(range.Start, end));

            foreach (var (state, i) in _states.WithIndex())
            {
                var stateCount = stateCounts.SingleOrDefault(x => x.State == state);
                if (stateCount == null)
                {
                    _data[i] = 0;
                    _labels[i] = Enum.GetName(state);
                }
                else
                {
                    _data[i] = stateCount.Count;
                    _labels[i] = $"{Enum.GetName(state)}: {stateCount.Count}";
                }
            }
        }
    }
}
