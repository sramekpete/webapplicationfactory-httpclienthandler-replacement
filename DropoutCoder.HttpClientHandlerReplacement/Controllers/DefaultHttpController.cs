namespace DropoutCoder.HttpClientHandlerReplacement.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;

    [ApiController]
    [Route("[controller]")]
    public class DefaultHttpController : ControllerBase
    {
        private readonly IHttpClientFactory _factory;

        public DefaultHttpController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _factory
                .CreateClient()
                .GetAsync(string.Empty);

            return StatusCode((int)response.StatusCode);
        }
    }
}
