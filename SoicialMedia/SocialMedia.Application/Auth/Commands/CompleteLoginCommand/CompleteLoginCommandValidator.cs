using FluentValidation;

namespace SocialMedia.Application.Auth.Commands.CompleteLoginCommand;

public class CompleteLoginCommandValidator : AbstractValidator<SocialMedia.Application.Auth.Commands.CompleteLoginCommand.CompleteLoginCommand>
{
    public CompleteLoginCommandValidator()
    {
        RuleFor(x => x.ValidationToken).NotEmpty();
    }
}