using MediatR;
using SocialMedia.Application.Common.Dto.Common;

namespace SocialMedia.Application.User.Commands.DeleteUserCommand;

public record DeleteUserCommand(string Id) : IRequest<DeleteResponseDto>;
