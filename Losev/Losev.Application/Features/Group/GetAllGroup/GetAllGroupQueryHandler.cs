using Losev.Application.Dtos.Group;
using Losev.Application.Extensions;
using Losev.Application.Features.Group.GetAllGroup;
using Losev.Domain.Repositories;
using MediatR;
using TS.Result;

public sealed class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupsQuery, Result<List<GroupDto>>>
{
    private readonly IGroupRepository _groupRepository;

    public GetAllGroupQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<List<GroupDto>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetAllAsync(cancellationToken);
        var activeGroups = groups
            .Where(group => !group.IsDeleted)
            .ToList();

        var groupDtos = activeGroups.Select(group => new GroupDto(
            group.Id,
            group.Name,
            group.GroupType,
            group.PassName,
            group.Url,
            group.AppUserId
        )).ToList();

        return Result<List<GroupDto>>.Succeed(groupDtos);
    }
}
