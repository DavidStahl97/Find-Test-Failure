using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.UITests
{
    public class WebUITestingOptions
    {
        public const string Position = "WebUITesting";

        public string SeleniumHubUrl { get; set; }

        public int NumberOfWorkers { get; set; }
    }
}
