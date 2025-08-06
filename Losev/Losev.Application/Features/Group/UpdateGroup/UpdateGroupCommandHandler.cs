using GenericRepository;
using Losev.Application.Extensions;
using Losev.Domain.Repositories;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.UpdateGroup;

public sealed class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Result<string>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id, cancellationToken);

        if (group is null || group.IsDeleted)
            return Result<string>.Failure("Grup bulunamadi");

        group.Name = request.Name;
        group.GroupType = request.GroupType;
        group.PassName = request.PassName;
        group.Url = request.Url;
        group.Password = request.Password;

        _groupRepository.Update(group);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Grup güncellendi");
    }
}