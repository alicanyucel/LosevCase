using FluentAssertions;
using Losev.Application.Features.User.GetUserById;
using Losev.Domain.Entities;
using Moq;

namespace Losev.Test.Features.User;

public class GetUserByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnUserDto_WhenUserExistsAndIsNotDeleted()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        var user = new AppUser
        {
            Id = userId,
            FirstName = "Ali",
            LastName = "Veli",
            IpAddress = "1.1.1.1",
            StatusSuccess = true,
            DateTime = DateTime.UtcNow,
            IsDeleted = false
        };

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new GetUserByIdQueryHandler(userRepositoryMock.Object);
        var query = new GetUserByIdQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Id.Should().Be(userId);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserIsNull()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AppUser)null);

        var handler = new GetUserByIdQueryHandler(userRepositoryMock.Object);
        var query = new GetUserByIdQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı bulunamadı veya silinmiş.");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserIsDeleted()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        var user = new AppUser
        {
            Id = userId,
            FirstName = "Ali",
            LastName = "Veli",
            IpAddress = "1.1.1.1",
            StatusSuccess = true,
            DateTime = DateTime.UtcNow,
            IsDeleted = true
        };

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new GetUserByIdQueryHandler(userRepositoryMock.Object);
        var query = new GetUserByIdQuery(userId);

        var result = await handler.Handle(query, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı bulunamadı veya silinmiş.");
    }
}
