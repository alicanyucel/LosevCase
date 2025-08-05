using Losev.Application.Extensions;
using Losev.Domain.Entities;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.User.GetAllUser;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<AppUser>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<AppUser>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        var activeUsers = users
            .Where(u => !u.IsDeleted)
            .Select(u => new AppUser
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                IpAddress = u.IpAddress,
                DateTime = u.DateTime
            })
            .ToList();

        activeUsers.ForEach(u => u.GetType().GetProperty("FirstNamw")?.SetValue(u, $"{u.FirstName} {u.LastName}"));

        return Result<List<AppUser>>.Succeed(activeUsers);
    }
}
