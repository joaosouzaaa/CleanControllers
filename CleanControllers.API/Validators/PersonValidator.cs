using CleanControllers.API.Entities;
using CleanControllers.API.Enums;
using CleanControllers.API.Extensions;
using CleanControllers.API.Validators.ValidatorUtils;
using FluentValidation;

namespace CleanControllers.API.Validators;

public sealed class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(p => p.Name).NotEmpty().Length(1, 100)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Name", "1 to 100"));

        RuleFor(p => p.Gender).Must(gender => EnumValidator.IsValidInputEnum<EGender>((ushort)gender))
            .WithMessage(EMessage.InvalidFormat.Description().FormatTo("Gender"));

        RuleFor(p => p.Email).EmailAddress().Length(1, 100)
            .WithMessage(EMessage.InvalidFormat.Description().FormatTo("Email"));

        RuleFor(p => p.Phone).Length(11)
            .WithMessage(EMessage.InvalidLength.Description().FormatTo("Phone", "11"));
    }
}
