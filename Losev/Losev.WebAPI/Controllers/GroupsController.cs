using Losev.WebAPI.Abstractions;
using MediatR;

namespace Losev.WebAPI.Controllers;

public class GroupsController : ApiController
{
    public GroupsController(IMediator mediator) : base(mediator)
    {
    }
}
