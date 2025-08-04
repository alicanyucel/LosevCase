using Losev.Application.Features.Auth.Login;
using Losev.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory; 

public sealed class AuthController : ApiController
{
    private readonly IMemoryCache _cache;

    public AuthController(IMediator mediator, IMemoryCache cache) : base(mediator)
    {
        _cache = cache;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        string cacheKey = $"login_{request.EmailOrUserName}";
        if (_cache.TryGetValue(cacheKey, out object? cachedResponse))
        {
            return StatusCode(200, cachedResponse);
        }

        var response = await _mediator.Send(request, cancellationToken);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        _cache.Set(cacheKey, response, cacheEntryOptions);

        return StatusCode(response.StatusCode, response);
    }
}
