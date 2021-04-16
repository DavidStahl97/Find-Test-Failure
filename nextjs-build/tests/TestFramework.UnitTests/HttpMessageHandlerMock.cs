using Autofac.Extras.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestFramework.UnitTests
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            NumberOfRequests++;

            ValidateHeaders(request.Content.Headers);

            var body = await request.Content.ReadAsStringAsync(cancellationToken);
            ValidateBody(body);

            return ReturnResponse();
        }

        public Action<string> ValidateBody { get; set; } = body => { };

        public Action<HttpContentHeaders> ValidateHeaders { get; set; } = headers => { };

        public Func<HttpResponseMessage> ReturnResponse { get; set; } = () => new HttpResponseMessage();

        public int NumberOfRequests { get; private set; }

        public static void MockHttpClient(HttpMessageHandlerMock messageHandlerMock, AutoMock mock)
        {
            var httpClient = new HttpClient(messageHandlerMock);

            mock.Mock<IHttpClientFactory>()
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);
        }
    }
}
