using Losev.Application.Dtos.User;
using Losev.Application.Extensions;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetAllUser;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserDto>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        var activeUsers = users
            .Where(u => !u.IsDeleted)
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                $"{u.FirstName} {u.LastName}",
                u.IpAddress,
                u.StatusSuccess,
                u.DateTime,
                u.IsDeleted
            ))
            .ToList();

        return Result<List<UserDto>>.Succeed(activeUsers);
    }
}
