using FluentValidation;
using Losev.Domain.Entities;

namespace Losev.Application.Features.User.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<AppUser>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("isim gerekli")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad gerekli")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Mail adresi gerekli")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.IpAddress)
            .NotEmpty().WithMessage("ip adresi gerekli")
            .MaximumLength(45); 

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Parola gereklli ");

    }
}
