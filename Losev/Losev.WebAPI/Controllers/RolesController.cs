using Losev.Application.Features.Role.GetRoles;
using Losev.Application.Features.Role.RoleSync;
using Losev.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Losev.WebAPI.Controllers;
[AllowAnonymous]
public sealed class RolesController : ApiController
{
    public RolesController(IMediator mediator) : base(mediator)
    {
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
        var roles = await _mediator.Send(new GetRolesQuery(), cancellationToken);
        return Ok(roles);
    }
}