using FluentAssertions;
using GenericRepository;
using Losev.Application.Features.User.CreateUser;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenUserIsCreatedSuccessfully()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher<AppUser>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var groupRepositoryMock = new Mock<IGroupRepository>();

        var command = new CreateUserCommand(
            FirstName: "Test",
            LastName: "User",
            IpAddress: "127.0.0.1",
            StatusSuccess: true,
            DateTime: DateTime.UtcNow,
            IsDeleted: false,
            GroupIds: null,
            Password: "password123"
        );

        passwordHasherMock
            .Setup(ph => ph.HashPassword(It.IsAny<AppUser>(), command.Password))
            .Returns("hashedPassword");

        unitOfWorkMock
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var handler = new CreateUserCommandHandler(
            userRepositoryMock.Object,
            passwordHasherMock.Object,
            unitOfWorkMock.Object,
            groupRepositoryMock.Object
        );

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Kullanıcı başarıyla oluşturuldu.");
        userRepositoryMock.Verify(repo => repo.Add(It.IsAny<AppUser>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailureResult_WhenSaveChangesFails()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        var passwordHasherMock = new Mock<IPasswordHasher<AppUser>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var groupRepositoryMock = new Mock<IGroupRepository>();

        var command = new CreateUserCommand(
            FirstName: "Test",
            LastName: "User",
            IpAddress: "127.0.0.1",
            StatusSuccess: true,
            DateTime: DateTime.UtcNow,
            IsDeleted: false,
            GroupIds: null,
            Password: "password123"
        );

        passwordHasherMock
            .Setup(ph => ph.HashPassword(It.IsAny<AppUser>(), command.Password))
            .Returns("hashedPassword");

        unitOfWorkMock
            .Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        var handler = new CreateUserCommandHandler(
            userRepositoryMock.Object,
            passwordHasherMock.Object,
            unitOfWorkMock.Object,
            groupRepositoryMock.Object
        );

        var result = await handler.Handle(command, CancellationToken.None);

        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı oluşturulamadı.");
        userRepositoryMock.Verify(repo => repo.Add(It.IsAny<AppUser>()), Times.Once);
        unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}