using GenericRepository;
using Losev.Application.Extensions;
using Losev.Domain.Repositories;
using MediatR;
using TS.Result;

namespace Losev.Application.Features.Group.DeleteGroup;

public sealed class DeleteGroupByIdCommandHandler : IRequestHandler<DeleteGroupCommand, Result<string>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGroupByIdCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(request.Id, cancellationToken);

        if (group is null || group.IsDeleted)
            return Result<string>.Failure("Grup bulunamaı.");

        group.IsDeleted = true;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Grup soft silindi.");
    }
}
