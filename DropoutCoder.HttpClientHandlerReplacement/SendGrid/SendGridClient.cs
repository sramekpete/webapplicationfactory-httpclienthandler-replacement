namespace DropoutCoder.HttpClientHandlerReplacement.SendGrid
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public class SendGridClient : ISendGridClient
    {
        private readonly HttpClient _httpClient;

        public SendGridClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            return await _httpClient
                .GetAsync(string.Empty);
        }
    }
}
