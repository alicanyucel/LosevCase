using Losev.Application.Features.Group.CreateGroup;
using Losev.Application.Features.Group.GetAllGroup;
using Losev.Application.Features.Group.UpdateGroup;
using Losev.Application.Features.User.DeleteUser;
using Losev.Application.Features.User.GetUserById;
using Losev.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Losev.WebAPI.Controllers;

[AllowAnonymous]
public class GroupsController : ApiController
{
    private readonly IMemoryCache _cache;
    public GroupsController(IMediator mediator, IMemoryCache cache) : base(mediator)
    {
        _cache = cache;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGroupById([FromRoute] Guid id)
    {
        string cacheKey = $"group_{id}";
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
    public async Task<IActionResult> CreateGroup([FromQuery] CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteGroupById(DeleteGroupByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroup([FromQuery] GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = "all_groups";
        if (!_cache.TryGetValue(cacheKey, out object? cachedResult))
        {
            var response = await _mediator.Send(request, cancellationToken);
            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));
            return Ok(response);
        }
        return Ok(cachedResult);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateGroupById(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}