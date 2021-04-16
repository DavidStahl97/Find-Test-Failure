using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Dtos.UITests.UIElements;
using TestFramework.Application.Types;

namespace TestFramework.Application.Requests.UITests
{
    public class PostRequest : IChangeOrCreate
    {
        public ChangeOrCreateUIElemenDto Dto { get; init; }
    }
}
