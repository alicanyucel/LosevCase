using FluentAssertions;
using Losev.Application.Features.User.DeleteUser;
using Losev.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using System.Threading;

public class UserDeleteByIdCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserIsDeletedSuccessfully()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        var user = new AppUser { Id = userId, IsDeleted = false };

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        userRepositoryMock
            .Setup(repo => repo.ExecuteUpdateAsync(It.IsAny<Expression<Func<SetPropertyCalls<AppUser>, SetPropertyCalls<AppUser>>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new DeleteUserCommandHandler(userRepositoryMock.Object);
        var command = new DeleteUserByIdCommand(userId);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Kullanıcı başarıyla soft delete yapıldı.");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserIsNotFound()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AppUser)null);

        var handler = new DeleteUserCommandHandler(userRepositoryMock.Object);
        var command = new DeleteUserByIdCommand(userId);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı bulunamadı veya zaten silinmiş.");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenUserIsAlreadyDeleted()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        var user = new AppUser { Id = userId, IsDeleted = true };

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var handler = new DeleteUserCommandHandler(userRepositoryMock.Object);
        var command = new DeleteUserByIdCommand(userId);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı bulunamadı veya zaten silinmiş.");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenExecuteUpdateFails()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = Guid.NewGuid();

        var user = new AppUser { Id = userId, IsDeleted = false };

        userRepositoryMock
            .Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        userRepositoryMock
            .Setup(repo => repo.ExecuteUpdateAsync(It.IsAny<Expression<Func<SetPropertyCalls<AppUser>, SetPropertyCalls<AppUser>>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        var handler = new DeleteUserCommandHandler(userRepositoryMock.Object);
        var command = new DeleteUserByIdCommand(userId);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Soft delete işlemi başarısız.");
    }
}
