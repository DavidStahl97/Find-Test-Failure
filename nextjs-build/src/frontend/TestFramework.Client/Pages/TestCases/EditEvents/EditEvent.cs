using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFramework.Client.WebAPI;

namespace TestFramework.Client.Pages.TestCases.EditEvents
{
    public abstract class EditEvent
    {
        public string Name { get; set; }

        public abstract string EditType { get; }
    }
}
