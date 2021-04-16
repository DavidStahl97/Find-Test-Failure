using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UITestCases;

namespace TestFramework.Application.Requests.UITests.UITestCases
{
    public class PutRequest : IChangeOrCreate
    {
        public int Id { get; init; }

        public ChangeOrCreateUITestCaseDto Dto { get; init; }
    }
}
