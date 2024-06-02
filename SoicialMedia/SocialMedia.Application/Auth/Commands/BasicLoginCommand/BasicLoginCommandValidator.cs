using FluentValidation;

namespace SocialMedia.Application.Auth.Commands.BasicLoginCommand;

public class BasicLoginCommandValidator : AbstractValidator<BasicLoginCommand>
{
    public BasicLoginCommandValidator()
    {
        RuleFor(x => x.User.Username).EmailAddress().NotEmpty();
        RuleFor(x => x.User.Password).NotEmpty();
    }
}