using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.IntegrationTests
{
    public class IntegrationTestsConfiguration
    {
        public Uri TestFrameworkUri { get; init; }

        public Uri TestWebAppUri { get; set; }
    }
}
