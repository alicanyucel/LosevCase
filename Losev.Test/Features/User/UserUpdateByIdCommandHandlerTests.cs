using FluentAssertions;
using GenericRepository;
using Losev.Application.Features.User.UpdateUser;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;

public class UserUpdateCommandByIdHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IGroupRepository> _groupRepositoryMock = new(); // kullanılmıyor ama ctor için gerekli
    private readonly Mock<IPasswordHasher<AppUser>> _passwordHasherMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    private readonly UpdateUserCommandHandler _handler;

    public UserUpdateCommandByIdHandlerTests()
    {
        _handler = new UpdateUserCommandHandler(
            _userRepositoryMock.Object,
            _groupRepositoryMock.Object,
            _passwordHasherMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_Should_Update_User_And_Return_Success()
    {
        var userId = Guid.NewGuid();
        var existingUser = new AppUser
        {
            Id = userId,
            FirstName = "Old",
            LastName = "Name",
            IsDeleted = false
        };

        var request = new UpdateUserCommand(
            userId,
            "New",
            "Name",
            "127.0.0.1",
            true,
            DateTime.UtcNow,
            false,
            null,
            "newpassword"
        );

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(existingUser);
        _passwordHasherMock.Setup(h => h.HashPassword(existingUser, request.Password)).Returns("hashed_password");
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccessful.Should().BeTrue();
        result.Data.Should().Be("Kullanıcı başarıyla güncellendi.");
        existingUser.FirstName.Should().Be("New");
        existingUser.Password.Should().Be("hashed_password");
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_User_Not_Found()
    {
        var userId = Guid.NewGuid();

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync((AppUser)null);

        var request = new UpdateUserCommand(
            userId,
            "Any",
            "User",
            null,
            false,
            DateTime.UtcNow,
            false,
            null,
            "pass"
        );

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı bulunamadı veya silinmiş.");
    }

    [Fact]
    public async Task Handle_Should_Return_Failure_When_Save_Fails()
    {
        var userId = Guid.NewGuid();
        var user = new AppUser { Id = userId, IsDeleted = false };

        var request = new UpdateUserCommand(
            userId,
            "Fail",
            "Save",
            null,
            false,
            DateTime.UtcNow,
            false,
            null,
            "pass"
        );

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(user);
        _passwordHasherMock.Setup(h => h.HashPassword(user, request.Password)).Returns("hashed");
        _unitOfWorkMock.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccessful.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Kullanıcı güncellenemedi.");
    }
}
