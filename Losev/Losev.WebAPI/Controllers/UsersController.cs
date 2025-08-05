using Losev.Application.Features.User.CreateUser;
using Losev.Application.Features.User.DeleteUser;
using Losev.Application.Features.User.GetAllUser;
using Losev.Application.Features.User.GetUserById;
using Losev.Application.Features.User.UpdateUser;
using Losev.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Losev.WebAPI.Controllers;

[AllowAnonymous]
public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var request = new GetUserByIdQuery(id); 
        var result = await _mediator.Send(request);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    [HttpPost]   
    public async Task<IActionResult> CreateUser([FromQuery] CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpDelete] 
    public async Task<IActionResult> DeleteUserById(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);

    }

    [HttpGet] 
    public async Task<IActionResult> GetAllUser([FromQuery] GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);

    }
    [HttpPut]
    public async Task<IActionResult> UpdateUserById(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);

    }

}