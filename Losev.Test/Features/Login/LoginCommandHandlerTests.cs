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

    private static IQueryable<AppUser> CreateQueryableUsers(IEnumerable<AppUser> users)
    {
        return users.AsQueryable();
    }

    [Fact]
    public async Task Handle_UserNotFound_ReturnsError()
    {
        var request = new LoginCommand("nonexistent", "password");
        _userManagerMock.Setup(x => x.Users).Returns(CreateQueryableUsers(new List<AppUser>()));
        var result = await _handler.Handle(request, default);
        Assert.False(result.IsSuccessful);
        Assert.Equal(new List<string> { "Kullanıcı bulunamadı" }, result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_WrongPassword_ReturnsError()
    {
        var user = new AppUser { UserName = "test", Email = "test@example.com" };
        var request = new LoginCommand("test", "wrongpassword");
        _userManagerMock.Setup(x => x.Users).Returns(CreateQueryableUsers(new List<AppUser> { user }));
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, request.Password, true))
            .ReturnsAsync(SignInResult.Failed);
        var result = await _handler.Handle(request, default);
        Assert.False(result.IsSuccessful);
        Assert.Contains("Şifreniz yanlış", result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_LoginNotAllowed_ReturnsNotAllowedMessage()
    {
        var user = new AppUser { UserName = "test", Email = "test@example.com" };
        var request = new LoginCommand("test", "any");
        _userManagerMock.Setup(x => x.Users).Returns(CreateQueryableUsers(new List<AppUser> { user }));
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, request.Password, true))
            .ReturnsAsync(SignInResult.NotAllowed);
        var result = await _handler.Handle(request, default);
        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.ErrorMessages);
        Assert.Contains("Mail adresiniz onaylı değil", result.ErrorMessages);
    }

    [Fact]
    public async Task Handle_SuccessfulLogin_ReturnsToken()
    {
        var user = new AppUser { UserName = "test", Email = "test@example.com" };
        var request = new LoginCommand("test", "correctpassword");
        _userManagerMock.Setup(x => x.Users).Returns(CreateQueryableUsers(new List<AppUser> { user }));
        _signInManagerMock.Setup(sm => sm.CheckPasswordSignInAsync(user, request.Password, true))
            .ReturnsAsync(SignInResult.Success);
        _jwtProviderMock.Setup(j => j.CreateToken(user))
            .ReturnsAsync(new LoginCommandResponse("fake.jwt.token", "fake.refresh.token", DateTime.UtcNow.AddDays(7)));
        var result = await _handler.Handle(request, default);
        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.Data);
        Assert.Equal("fake.jwt.token", result.Data.Token);
    }
}