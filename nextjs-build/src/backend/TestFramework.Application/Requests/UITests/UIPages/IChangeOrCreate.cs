using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;

namespace TestFramework.Application.Requests.UITests.UIPages
{
    public interface IChangeOrCreate
    {
        ChangeOrCreateUIPageDto Dto { get; init; }
    }
}
