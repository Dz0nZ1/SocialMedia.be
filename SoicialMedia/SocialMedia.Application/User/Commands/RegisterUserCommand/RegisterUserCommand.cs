using MediatR;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.User.Commands.RegisterUserCommand;

public record RegisterUserCommand(CreateUserDetailsDto User) : IRequest<UserDetailsDto?>;