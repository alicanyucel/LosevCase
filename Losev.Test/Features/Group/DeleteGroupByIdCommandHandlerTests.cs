using FluentAssertions;
using GenericRepository;
using Losev.Application.Extensions;
using Losev.Application.Features.Group.DeleteGroup;
using Losev.Domain.Entities;
using Losev.Domain.Enums;
using Losev.Domain.Repositories;
using Moq;

public class DeleteGroupByIdCommandHandlerTests
{
    private readonly Mock<IGroupRepository> _groupRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteGroupByIdCommandHandler _handler;

    public DeleteGroupByIdCommandHandlerTests()
    {
        _groupRepositoryMock = new Mock<IGroupRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteGroupByIdCommandHandler(_groupRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_SoftDeleteGroup_And_ReturnSuccess()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            IsDeleted = false,
            GroupType = GroupType.MobileGroup
        };

        _groupRepositoryMock.Setup(x => x.GetByIdAsync(group.Id, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(group);

        var command = new DeleteGroupCommand(group.Id);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Grup soft silindi.");
        group.IsDeleted.Should().BeTrue();

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenGroupNotFound()
    {
        _groupRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                            .ReturnsAsync((Group)null);

        var command = new DeleteGroupCommand(Guid.NewGuid());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Grup bulunamaı.");

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenGroupIsAlreadyDeleted()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            IsDeleted = true,
            GroupType = GroupType.MobileGroup
        };

        _groupRepositoryMock.Setup(x => x.GetByIdAsync(group.Id, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(group);

        var command = new DeleteGroupCommand(group.Id);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Grup bulunamaı.");

        _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
