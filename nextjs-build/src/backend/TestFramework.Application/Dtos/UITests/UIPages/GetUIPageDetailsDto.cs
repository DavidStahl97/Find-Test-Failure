using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos.UITests.UIElements;

namespace TestFramework.Application.Dtos.UITests.UIPages
{
    public class GetUIPageDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PaginationResult<GetUIElementDto> Page { get; set; }
    }
}
