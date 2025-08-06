using GenericRepository;
using Losev.Application.Extensions;
using Losev.Application.Features.User.UpdateUser;
using Losev.Domain.Entities;
using Losev.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IGroupRepository _groupRepository;
    private readonly IPasswordHasher<AppUser> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(
        IUserRepository userRepository,
        IGroupRepository groupRepository,
        IPasswordHasher<AppUser> passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _groupRepository = groupRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null || user.IsDeleted)
            return Result<string>.Failure("Kullanıcı bulunamadı veya silinmiş.");

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.IpAddress = request.IpAddress;
        user.StatusSuccess = request.StatusSuccess;
        user.DateTime = request.DateTime;
        user.IsDeleted = request.IsDeleted;
        user.Password = _passwordHasher.HashPassword(user, request.Password);

       

        _userRepository.Update(user);

        var commitResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (commitResult > 0)
        return Result<string>.Succeed("Kullanıcı başarıyla güncellendi.");
        return Result<string>.Failure("Kullanıcı güncellenemedi.");
    }
}
