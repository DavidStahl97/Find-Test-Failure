using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Domain.UITesting.Run
{
    public enum UITestEventState
    {
        NotStarted,
        Started,
        Failure,
        Cancel,
        Completed
    }
}
