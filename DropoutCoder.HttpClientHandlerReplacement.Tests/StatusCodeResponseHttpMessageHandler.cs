namespace DropoutCoder.HttpClientHandlerReplacement.Tests
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    internal class StatusCodeResponseHttpMessageHandler : HttpMessageHandler
    {
        public StatusCodeResponseHttpMessageHandler(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(StatusCode)
            {
                Content = request.Content
            };

            return Task.FromResult(response);
        }
    }
}
