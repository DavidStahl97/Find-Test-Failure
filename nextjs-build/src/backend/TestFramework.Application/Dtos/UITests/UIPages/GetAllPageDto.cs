using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Dtos.UITests.UIPages
{
    public class GetAllPageDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<GetAllUIElementDto> UIElements { get; set; }
    }
}
