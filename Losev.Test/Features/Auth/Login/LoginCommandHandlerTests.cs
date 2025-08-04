using Losev.Application.Features.Auth.Login;
using Losev.Application.Services;
using Losev.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

public class LoginCommandHandlerTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerMock;
    private readonly Mock<SignInManager<AppUser>> _signInManagerMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly LoginCommandHandler _handler;

    public LoginCommandHandlerTests()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        _userManagerMock = new Mock<UserManager<AppUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);

        var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
        _signInManagerMock = new Mock<SignInManager<AppUser>>(
            _userManagerMock.Object,
            contextAccessorMock.Object,
            claimsFactoryMock.Object,
            null, null, null, null);

        _jwtProviderMock = new Mock<IJwtProvider>();
        _handler = new LoginCommandHandler(
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _jwtProviderMock.Object
        );
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsError()
    {
        var request = new LoginCommand("nonexistent", "password");
        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<AppUser>().AsQueryable());
        var result = await _handler.Handle(request, default);
        Assert.False(result.IsSuccess);
        Assert.Equal("Kullanıcı bulunamadı", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_WrongPassword_ReturnsError()
    {
        var user = new AppUser { UserName = "test", Email = "test@example.com" };
        var request = new LoginCommand("test", "wrongpassword");
        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<AppUser> { user }.AsQueryable());
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, request.Password, true))
            .ReturnsAsync(SignInResult.Failed);
        var result = await _handler.Handle(request, default);
        Assert.False(result.IsSuccess);
        Assert.Equal("Şifreniz yanlış", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_LoginNotAllowed_ReturnsNotAllowedMessage()
    {
        var user = new AppUser { UserName = "test", Email = "test@example.com" };
        var request = new LoginCommand("test", "any");
        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<AppUser> { user }.AsQueryable());
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, request.Password, true))
            .ReturnsAsync(SignInResult.NotAllowed);
        var result = await _handler.Handle(request, default);
        Assert.False(result.IsSuccess);
        Assert.Equal("Mail adresiniz onaylı değil", result.ErrorMessage);
    }

    [Fact]
    public async Task Handle_SuccessfulLogin_ReturnsToken()
    {
        var user = new AppUser { UserName = "test", Email = "test@example.com" };
        var request = new LoginCommand("test", "correctpassword");
        _userManagerMock.Setup(um => um.Users)
            .Returns(new List<AppUser> { user }.AsQueryable());
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, request.Password, true))
            .ReturnsAsync(SignInResult.Success);
        _jwtProviderMock.Setup(j => j.CreateToken(user))
            .ReturnsAsync(new LoginCommandResponse("fake.jwt.token", "fake.refresh.token", DateTime.UtcNow.AddDays(7)));
        var result = await _handler.Handle(request, default);
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal("fake.jwt.token", result.Data.Token);
    }
}