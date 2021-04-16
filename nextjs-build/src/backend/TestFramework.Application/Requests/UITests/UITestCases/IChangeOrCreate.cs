using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;

namespace TestFramework.Application.Requests.UITests.UITestCases
{
    public interface IChangeOrCreate
    {
        ChangeOrCreateUITestCaseDto Dto { get; init; }
    }
}
