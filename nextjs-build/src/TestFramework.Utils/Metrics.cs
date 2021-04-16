using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Utils
{
    public static class Metrics
    {
        public static (TimeSpan measure, T Response) Measure<T>(Func<T> execute)
        {
            var eventWatch = new Stopwatch();
            eventWatch.Start();

            var response = execute();

            eventWatch.Stop();
            var timeSpan = new TimeSpan(eventWatch.ElapsedTicks);
            return (timeSpan, response);
        }

        public static async Task<(TimeSpan measure, T Response)> MeasureAsync<T>(Func<Task<T>> execute)
        {
            var eventWatch = new Stopwatch();
            eventWatch.Start();

            var response = await execute();

            eventWatch.Stop();
            var timeSpan = new TimeSpan(eventWatch.ElapsedTicks);
            return (timeSpan, response);
        } 
    }
}
