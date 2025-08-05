using GenericRepository;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;
namespace Losev.Application.Features.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<AppUser> _passwordHasher;
    private readonly IUnitOfWork _unitofwork;
    private readonly IGroupRepository _groupRepository;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher<AppUser> passwordHasher,IUnitOfWork unitOfWork,IGroupRepository groupRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitofwork = unitOfWork;
        _groupRepository = groupRepository;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            RefreshToken = request.RefreshToken,
            RefreshTokenExpires = request.RefreshTokenExpires,
            IpAddress = request.IpAddress,
            StatusSuccess = request.StatusSuccess,
            DateTime = request.DateTime,
            IsDeleted = request.IsDeleted,
            PasswordSalt = request.PasswordSalt 
        };

    
        user.Password = _passwordHasher.HashPassword(user, request.Password);
        if (request.GroupIds != null && request.GroupIds.Any())
        {
            var groups = await _groupRepository.GetAll()
                .Where(g => request.GroupIds.Contains(g.Id))
                .ToListAsync(cancellationToken);

            user.Groups = groups;
        }

        _userRepository.Add(user);

        var saveResult = await _unitofwork.SaveChangesAsync(cancellationToken);

        if (saveResult > 0)
            return Result<string>.Succeed("Kullanıcı başarıyla oluşturuldu.");
        else
            return Result<string>.Failure("Kullanıcı oluşturulamadı.");
    }
}
