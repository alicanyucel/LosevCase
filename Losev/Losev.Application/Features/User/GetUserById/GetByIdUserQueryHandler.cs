using Losev.Application.Dtos.User;
using Losev.Application.Extensions;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null || user.IsDeleted)
        {
            return Result<UserDto>.Failure("Kullanıcı bulunamadı veya silinmiş.");
        }

        var userDto = new UserDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.FullName,
            user.IpAddress,
            user.StatusSuccess,
            user.DateTime,
            user.IsDeleted
        );

        return Result<UserDto>.Succeed(userDto);
    }
}
