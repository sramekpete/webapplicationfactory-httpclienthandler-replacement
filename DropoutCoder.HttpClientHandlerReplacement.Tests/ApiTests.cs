namespace DropoutCoder.HttpClientHandlerReplacement.Tests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    [TestClass]
    public class ApiTests
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiTests()
        {
            _factory = new WebApplicationFactory<Program>();
        }

        [TestMethod]
        public async Task DefaultHttpClientTest()
        {
            var client = _factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .ConfigureHttpClientDefaults(builder =>
                                builder.ConfigurePrimaryHttpMessageHandler(() => new StatusCodeResponseHttpMessageHandler(HttpStatusCode.InternalServerError))
                             );

                        services
                            .AddHttpClient<HttpClient>(HttpClientNames.Google)
                            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());

                    })).CreateClient();


            var @default = await client.GetAsync("/DefaultHttp");

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.InternalServerError);

            var google = await client.GetAsync("/GoogleHttp");

            Assert.IsTrue(google.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task NamedHttpClientTest()
        {
            var client = _factory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .AddHttpClient<HttpClient>(HttpClientNames.Google)
                            .ConfigurePrimaryHttpMessageHandler(() => new StatusCodeResponseHttpMessageHandler(HttpStatusCode.InternalServerError));

                    })).CreateClient();

            var @default = await client.GetAsync("/DefaultHttp");

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.OK);

            var google = await client.GetAsync("/GoogleHttp");

            Assert.IsTrue(google.StatusCode == HttpStatusCode.InternalServerError);
        }
    }
}