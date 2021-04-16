using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Application.Requests.UITests.UIPages
{
    public class GetDetailsRequest
    {
        public int Id { get; set; }

        public PaginationRequest Pagination { get; set; }
    }
}
