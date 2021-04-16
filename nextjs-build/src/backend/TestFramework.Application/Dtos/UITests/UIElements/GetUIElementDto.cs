using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Domain.UITesting;

namespace TestFramework.Application.Dtos.UITests.UIElements
{
    public class GetUIElementDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FindByMethod FindByMethod { get; set; }

        public string FindBy { get; set; }

        public int PageId { get; set; }
    }
}
