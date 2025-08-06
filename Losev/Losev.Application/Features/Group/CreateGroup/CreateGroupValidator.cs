using System;
using System.Collections.Generic;
using FluentValidation;
using Losev.Domain.Entities;

public class CreateGroupCommandValidator : AbstractValidator<Group>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100);

        RuleFor(x => x.GroupType)
            .IsInEnum().WithMessage("Invalid group type.");

        RuleFor(x => x.PassName)
            .NotEmpty().WithMessage("PassName is required.")
            .MaximumLength(100);

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required.")
            .MaximumLength(200)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Invalid URL format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.AppUserId)
            .NotEmpty().WithMessage("AppUserId is required.");
    }
}
