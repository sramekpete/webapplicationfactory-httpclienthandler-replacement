namespace DropoutCoder.HttpClientHandlerReplacement.Controllers
{
    using DropoutCoder.HttpClientHandlerReplacement;
    using DropoutCoder.HttpClientHandlerReplacement.SendGrid;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class TypedHttpClientController : ControllerBase
    {
        private readonly ISendGridClient _client;

        public TypedHttpClientController(ISendGridClient client)
        {
            _client = client;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _client
                .SendAsync();

            return StatusCode((int)response.StatusCode);
        }
    }
}
