using Losev.Application.Extensions;
using MediatR;
using TS.Result;


namespace Losev.Application.Features.User.DeleteUser;


public class DeleteUserCommandHandler : IRequestHandler<DeleteUserByIdCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null || user.IsDeleted)
            return Result<string>.Failure("Kullanıcı bulunamadı veya zaten silinmiş.");
        var success = await _userRepository.ExecuteUpdateAsync(
            u => u.SetProperty(x => x.IsDeleted, true), 
            cancellationToken
        );

        if (success > 0) 
            return Result<string>.Succeed("Kullanıcı başarıyla soft delete yapıldı.");
        else
            return Result<string>.Failure("Soft delete işlemi başarısız.");
    }
}
