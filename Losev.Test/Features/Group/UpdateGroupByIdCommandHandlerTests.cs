using GenericRepository;
using Losev.Application.Features.Group.UpdateGroup;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using Moq;

public class UpdateGroupCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenGroupIsNull()
    {
        var mockRepo = new Mock<IGroupRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Group?)null);

        var handler = new UpdateGroupCommandHandler(mockRepo.Object, mockUnitOfWork.Object);

        var result = await handler.Handle(new UpdateGroupCommand(Guid.NewGuid(), "Name", default, "PassName", "Url", "Password"), CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Contains("Grup bulunamadi", result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenGroupIsDeleted()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            IsDeleted = true,
            Name = "OldName",
            GroupType = default
        };

        var mockRepo = new Mock<IGroupRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockRepo.Setup(r => r.GetByIdAsync(group.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

        var handler = new UpdateGroupCommandHandler(mockRepo.Object, mockUnitOfWork.Object);

        var result = await handler.Handle(new UpdateGroupCommand(group.Id, "Name", default, "PassName", "Url", "Password"), CancellationToken.None);

        Assert.False(result.IsSuccessful);
        Assert.Contains("Grup bulunamadi", result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_ShouldUpdateGroupAndReturnSuccess()
    {
        var group = new Group
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Name = "OldName",
            GroupType = default,
            PassName = "OldPassName",
            Url = "OldUrl",
            Password = "OldPassword"
        };

        var mockRepo = new Mock<IGroupRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockRepo.Setup(r => r.GetByIdAsync(group.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(group);

        mockRepo.Setup(r => r.Update(It.IsAny<Group>()));

        mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
                      .ReturnsAsync(1);

        var handler = new UpdateGroupCommandHandler(mockRepo.Object, mockUnitOfWork.Object);

        var command = new UpdateGroupCommand(group.Id, "NewName", default, "NewPassName", "NewUrl", "NewPassword");

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result.IsSuccessful);
        Assert.Equal("Grup güncellendi", result.Data);
        Assert.Equal("NewName", group.Name);
        Assert.Equal(command.GroupType, group.GroupType);
        Assert.Equal("NewPassName", group.PassName);
        Assert.Equal("NewUrl", group.Url);
        Assert.Equal("NewPassword", group.Password);

        mockRepo.Verify(r => r.Update(group), Times.Once);
        mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
