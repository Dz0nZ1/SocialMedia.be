using FluentValidation;

namespace SocialMedia.Application.Auth.Commands.BeginLoginCommand;

public class BeginLoginCommandValidator : AbstractValidator<SocialMedia.Application.Auth.Commands.BeginLoginCommand.BeginLoginCommand>
{
    public BeginLoginCommandValidator()
    {
        RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty();
    }
}