using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIElements;

namespace TestFramework.Application.Requests.UITests
{
    public interface IChangeOrCreate
    {
        ChangeOrCreateUIElemenDto Dto { get; init; }
    }
}
