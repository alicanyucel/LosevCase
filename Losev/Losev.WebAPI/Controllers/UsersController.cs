using Losev.Application.Features.User.CreateUser;
using Losev.Application.Features.User.DeleteUser;
using Losev.Application.Features.User.GetAllUser;
using Losev.Application.Features.User.GetUserById;
using Losev.Application.Features.User.UpdateUser;
using Losev.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Losev.WebAPI.Controllers;

[AllowAnonymous]
public class UsersController : ApiController
{
    private readonly IMemoryCache _cache;
    public UsersController(IMediator mediator, IMemoryCache cache) : base(mediator)
    {
        _cache = cache;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        string cacheKey = $"user_{id}";
        if (!_cache.TryGetValue(cacheKey, out object? cachedResult))
        {
            var request = new GetUserByIdQuery(id);
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return NotFound();
            }
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));
            return Ok(result);
        }
        return Ok(cachedResult);
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
        string cacheKey = "all_users";
        if (!_cache.TryGetValue(cacheKey, out object? cachedResult))
        {
            var response = await _mediator.Send(request, cancellationToken);
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));
            return Ok(response);
        }
        return Ok(cachedResult);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateUserById(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}