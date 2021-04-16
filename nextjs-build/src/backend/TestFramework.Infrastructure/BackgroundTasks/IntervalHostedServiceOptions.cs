using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.BackgroundTasks
{
    public class IntervalHostedServiceOptions
    {
        public TimeSpan Interval { get; init; }
    }
}
