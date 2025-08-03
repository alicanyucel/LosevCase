using Losev.Application.Features.Auth.Login;
using Losev.Domain.Entities;

namespace Losev.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
