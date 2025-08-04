using Losev.Application.Features.Role.GetRoles;
using Losev.Application.Features.Role.RoleSync;
using Losev.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Losev.WebAPI.Controllers;
[AllowAnonymous]
public sealed class RolesController : ApiController
{
    private readonly IMemoryCache _cache;
    public RolesController(IMediator mediator, IMemoryCache cache) : base(mediator)
    {
        _cache = cache;
    }

    [HttpPost]
    public async Task<IActionResult> Sync(RoleSyncCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<ActionResult<List<AppRole>>> GetRoles(CancellationToken cancellationToken)
    {
        const string cacheKey = "roles_list";
        if (_cache.TryGetValue(cacheKey, out List<AppRole>? cachedRoles))
        {
            return Ok(cachedRoles);
        }
        var roles = await _mediator.Send(new GetRolesQuery(), cancellationToken);
        var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
        _cache.Set(cacheKey, roles, cacheEntryOptions);
        return Ok(roles);
    }
}