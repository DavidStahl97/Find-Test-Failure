using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.UITests
{
    public class RemoteWebDriverApiOptions
    {
        public const string Position = "RemoteWebDriverApi";

        public Uri Uri { get; set; }

        public int MaxRetries { get; set; }

        public int WaitInMilliseconds { get; set; }
    }
}
