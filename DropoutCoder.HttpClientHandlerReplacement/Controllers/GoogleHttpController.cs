namespace DropoutCoder.HttpClientHandlerReplacement.Controllers
{
    using DropoutCoder.HttpClientHandlerReplacement;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class GoogleHttpController : ControllerBase
    {
        private readonly IHttpClientFactory _factory;

        public GoogleHttpController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _factory
                .CreateClient(HttpClientNames.Google)
                .GetAsync(string.Empty);

            return StatusCode((int)response.StatusCode);
        }
    }
}
