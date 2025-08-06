using Losev.Application.Dtos.Group;
using Losev.Application.Extensions;
using Losev.Domain.Repositories;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.GetByIdGroup;

public sealed class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, Result<GroupDto>>
{
    private readonly IGroupRepository _groupRepository;

    public GetGroupByIdQueryHandler(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Result<GroupDto>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id, cancellationToken);
        if (group is null || group.IsDeleted)
            return Result<GroupDto>.Failure("Grup bulunamadı.");

        var groupDto = new GroupDto(
            group.Id,
            group.Name,
            group.GroupType,
            group.PassName,
            group.Url,
            group.AppUserId
        );

        return Result<GroupDto>.Succeed(groupDto);
    }
}
