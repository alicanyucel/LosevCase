using FluentAssertions;
using GenericRepository;
using Losev.Application.Features.Group.CreateGroup;
using Losev.Domain.Entities;
using Losev.Domain.Enums;
using Losev.Domain.Repositories;
using Moq;


public class CreateGroupCommandHandlerTests
{
    private readonly Mock<IGroupRepository> _groupRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CreateGroupCommandHandler _handler;

    public CreateGroupCommandHandlerTests()
    {
        _groupRepositoryMock = new Mock<IGroupRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateGroupCommandHandler(_groupRepositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Group_And_Return_Success()
    {
        var command = new CreateGroupCommand(
            Name: "Test Group",
            GroupType: GroupType.DesktopGroup,
            PassName: "test-group",
            Url: "http://testgroup.com",
            Password: "secret",
            AppUserId: Guid.NewGuid()
        );

        var result = await _handler.Handle(command, CancellationToken.None);
        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Grup eklendi");

        _groupRepositoryMock.Verify(repo =>
            repo.AddAsync(It.Is<Group>(g =>
                g.Name == command.Name &&
                g.GroupType == command.GroupType &&
                g.PassName == command.PassName &&
                g.Url == command.Url &&
                g.Password == command.Password &&
                g.AppUserId == command.AppUserId
            ), It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWorkMock.Verify(uow =>
            uow.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
