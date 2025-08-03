using Losev.WebAPI.Abstractions;
using MediatR;

namespace Losev.WebAPI.Controllers;


public class UsersController : ApiController
{
    public UsersController(IMediator mediator) : base(mediator)
    {
    }
}
