using GenericRepository;
using Losev.Application.Features.Group.CreateGroup;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using MediatR;
using TS.Result;

public sealed class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<string>>
{
    private readonly IGroupRepository _groupRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGroupCommandHandler(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
    {
        _groupRepository = groupRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            GroupType = request.GroupType,
            PassName = request.PassName,
            Url = request.Url,
            Password = request.Password,
            AppUserId = request.AppUserId
        };

        await _groupRepository.AddAsync(group, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succeed("Grup eklendi");
    }
}