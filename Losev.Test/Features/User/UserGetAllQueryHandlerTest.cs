using FluentAssertions;
using Losev.Application.Extensions;
using Losev.Application.Features.User.GetAllUser;
using Losev.Domain.Entities;
using Moq;

public class GetAllUsersQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnActiveUsers_WhenUsersExist()
    {
        var userRepositoryMock = new Mock<IUserRepository>();

        var users = new List<AppUser>
        {
            new AppUser { Id = Guid.NewGuid(), FirstName = "Ali", LastName = "Veli", IpAddress = "1.1.1.1", StatusSuccess = true, DateTime = DateTime.UtcNow, IsDeleted = false },
            new AppUser { Id = Guid.NewGuid(), FirstName = "Ayşe", LastName = "Fatma", IpAddress = "2.2.2.2", StatusSuccess = false, DateTime = DateTime.UtcNow, IsDeleted = true }
        };

        userRepositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        var handler = new GetAllUsersQueryHandler(userRepositoryMock.Object);

        var result = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().HaveCount(1);
        result.Data.First().FirstName.Should().Be("Ali");
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenAllUsersAreDeleted()
    {
        var userRepositoryMock = new Mock<IUserRepository>();

        var users = new List<AppUser>
        {
            new AppUser { Id = Guid.NewGuid(), FirstName = "Ali", LastName = "Veli", IpAddress = "1.1.1.1", StatusSuccess = true, DateTime = DateTime.UtcNow, IsDeleted = true }
        };

        userRepositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        var handler = new GetAllUsersQueryHandler(userRepositoryMock.Object);
        var result = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().BeEmpty();
    }
}
