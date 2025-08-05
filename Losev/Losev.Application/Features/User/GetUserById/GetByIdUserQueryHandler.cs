using Losev.Application.Extensions;
using Losev.Domain.Entities;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<AppUser>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<AppUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null || user.IsDeleted)
        {
            return Result<AppUser>.Failure("Kullanıcı bulunamadı veya silinmiş.");
        }

        return Result<AppUser>.Succeed(user);
    }
}
