using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestFramework.Application.Dtos;
using TestFramework.Application.Types;

namespace TestFramework.Application.Requests
{
    public class PaginationRequest
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        private string _search = string.Empty;
        public string Search         
        {
            get => _search;
            init => _search = value is null ? string.Empty : value;
        }
    }
}
