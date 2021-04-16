using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIPages;

namespace TestFramework.Application.Requests.UITests.UIPages
{
    public class PutRequest : IChangeOrCreate
    {
        public int Id { get; init; }

        public ChangeOrCreateUIPageDto Dto { get; init; }
    }
}
