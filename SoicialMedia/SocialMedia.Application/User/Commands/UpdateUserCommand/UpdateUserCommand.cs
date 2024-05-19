using MediatR;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.User.Commands.UpdateUserCommand;

public record UpdateUserCommand(UpdateUserDetailsDto User) : IRequest<UserDetailsDto?>;