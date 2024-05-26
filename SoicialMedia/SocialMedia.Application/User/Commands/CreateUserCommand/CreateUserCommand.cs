using MediatR;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.User.Commands.CreateUserCommand;

public record CreateUserCommand(CreateUserDetailsDto User, List<string> Roles) : IRequest<UserDetailsDto?>;