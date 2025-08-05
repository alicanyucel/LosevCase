using FluentValidation;
using Losev.Domain.Entities;

namespace Losev.Application.Features.User.UpdateUser;

public class UpdateUserValidator : AbstractValidator<AppUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Kullanıcı id si gerekli");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("İsim gerekli")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyisim gerekli")
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Mail gerekli")
            .EmailAddress().WithMessage("Geçerli email adresi giriniz.");

        RuleFor(x => x.IpAddress)
            .NotEmpty().WithMessage("İp adresi gereklidir.")
            .MaximumLength(45); 

        RuleFor(x => x.Password)
            .MaximumLength(255).When(x => !string.IsNullOrWhiteSpace(x.Password));

        RuleFor(x=>x.Password).NotEmpty().WithMessage("Password Gerekli").MaximumLength(60);

    }
}
