using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UITestCases
{
    public class ChangeOrCreateUITestCaseDto
    {
        public string Name { get; set; }

        public Uri StartUrl { get; set; }

        public TimeSpan DefaultWaitForUIElement { get; set; }

        public bool RunsPeriodically { get; set; }
    }
}
