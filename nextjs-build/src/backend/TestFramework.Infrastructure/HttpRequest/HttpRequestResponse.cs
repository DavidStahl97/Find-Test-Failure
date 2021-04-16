using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework.Infrastructure.HttpRequest
{
    public class HttpRequestResponse
    {
        public HttpStatusCode StatusCode { get; init; }

        public bool WasSuccessful { get; init; }
    }

    public class HttpRequestResponse<T> : HttpRequestResponse
    {
        public T Resource { get; init; }
    }
}
