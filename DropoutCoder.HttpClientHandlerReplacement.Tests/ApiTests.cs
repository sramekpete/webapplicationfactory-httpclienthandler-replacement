namespace DropoutCoder.HttpClientHandlerReplacement.Tests
{
    using DropoutCoder.HttpClientHandlerReplacement.SendGrid;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using System.Net;

    [TestClass]
    public class ApiTests
    {
        private readonly WebApplicationFactory<Program> _defaultFactory;
        private readonly ApiApplicationFactory _customFactory;

        public ApiTests()
        {
            _defaultFactory = new WebApplicationFactory<Program>();
            _customFactory = new ApiApplicationFactory();
        }

        [TestMethod]
        public async Task DefaultHttpClientTestWithDefaultFactory()
        {
            var client = _defaultFactory.WithWebHostBuilder(builder =>
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

                        services
                            .AddHttpClient<ISendGridClient, SendGridClient>()
                            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());

                    })).CreateClient();


            var @default = await client.GetAsync(EndpointUrls.DefaultHttpClientEndpoint);

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.InternalServerError);

            var named = await client.GetAsync(EndpointUrls.NamedHttpClientEndpoint);

            Assert.IsTrue(named.StatusCode == HttpStatusCode.OK);

            var typed = await client.GetAsync(EndpointUrls.TypedHttpClientEndpoint);

            Assert.IsTrue(typed.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task NamedHttpClientTestWithDefaultFactory()
        {
            var client = _defaultFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .AddHttpClient<HttpClient>(HttpClientNames.Google)
                            .ConfigurePrimaryHttpMessageHandler(() => new StatusCodeResponseHttpMessageHandler(HttpStatusCode.InternalServerError));
                    })).CreateClient();

            var @default = await client.GetAsync(EndpointUrls.DefaultHttpClientEndpoint);

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.OK);

            var named = await client.GetAsync(EndpointUrls.NamedHttpClientEndpoint);

            Assert.IsTrue(named.StatusCode == HttpStatusCode.InternalServerError);

            var typed = await client.GetAsync(EndpointUrls.TypedHttpClientEndpoint);

            Assert.IsTrue(typed.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task TypedHttpClientTestWithDefaultFactory()
        {
            var client = _defaultFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .AddHttpClient<ISendGridClient, SendGridClient>()
                            .ConfigurePrimaryHttpMessageHandler(() => new StatusCodeResponseHttpMessageHandler(HttpStatusCode.InternalServerError));
                    })).CreateClient();

            var @default = await client.GetAsync(EndpointUrls.DefaultHttpClientEndpoint);

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.OK);

            var named = await client.GetAsync(EndpointUrls.NamedHttpClientEndpoint);

            Assert.IsTrue(named.StatusCode == HttpStatusCode.OK);

            var typed = await client.GetAsync(EndpointUrls.TypedHttpClientEndpoint);

            Assert.IsTrue(typed.StatusCode == HttpStatusCode.InternalServerError);
        }

        [TestMethod]
        public async Task DefaultHttpClientTestWithCustomFactory()
        {
            var client = _customFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .AddHttpClient<HttpClient>(HttpClientNames.Google)
                            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());

                        services
                            .AddHttpClient<ISendGridClient, SendGridClient>()
                            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());

                    })).CreateClient();


            var @default = await client.GetAsync(EndpointUrls.DefaultHttpClientEndpoint);

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.InternalServerError);

            var named = await client.GetAsync(EndpointUrls.NamedHttpClientEndpoint);

            Assert.IsTrue(named.StatusCode == HttpStatusCode.OK);

            var typed = await client.GetAsync(EndpointUrls.TypedHttpClientEndpoint);

            Assert.IsTrue(typed.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task NamedHttpClientTestWithCustomFactory()
        {
            var client = _customFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .ConfigureHttpClientDefaults(builder =>
                                builder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
                             );

                        services
                            .AddHttpClient<HttpClient>(HttpClientNames.Google)
                            .ConfigurePrimaryHttpMessageHandler(() => new StatusCodeResponseHttpMessageHandler(HttpStatusCode.InternalServerError));
                    })).CreateClient();

            var @default = await client.GetAsync(EndpointUrls.DefaultHttpClientEndpoint);

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.OK);

            var named = await client.GetAsync(EndpointUrls.NamedHttpClientEndpoint);

            Assert.IsTrue(named.StatusCode == HttpStatusCode.InternalServerError);

            var typed = await client.GetAsync(EndpointUrls.TypedHttpClientEndpoint);

            Assert.IsTrue(typed.StatusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task TypedHttpClientTestWithCustomFactory()
        {
            var client = _defaultFactory.WithWebHostBuilder(builder =>
                builder.ConfigureTestServices(
                    services =>
                    {
                        services
                            .AddHttpClient<ISendGridClient, SendGridClient>()
                            .ConfigurePrimaryHttpMessageHandler(() => new StatusCodeResponseHttpMessageHandler(HttpStatusCode.InternalServerError));
                    })).CreateClient();

            var @default = await client.GetAsync(EndpointUrls.DefaultHttpClientEndpoint);

            Assert.IsTrue(@default.StatusCode == HttpStatusCode.OK);

            var named = await client.GetAsync(EndpointUrls.NamedHttpClientEndpoint);

            Assert.IsTrue(named.StatusCode == HttpStatusCode.OK);

            var typed = await client.GetAsync(EndpointUrls.TypedHttpClientEndpoint);

            Assert.IsTrue(typed.StatusCode == HttpStatusCode.InternalServerError);
        }
    }
}