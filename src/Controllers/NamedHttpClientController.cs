namespace HttpClientHandlerReplacement.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class NamedHttpClientController : ControllerBase
{
    private readonly IHttpClientFactory _factory;

    public NamedHttpClientController(IHttpClientFactory factory)
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
