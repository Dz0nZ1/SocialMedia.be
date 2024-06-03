using MediatR;

namespace SocialMedia.Application.Auth.Commands.LogoutCommand;

public record LogoutCommand(string UserId) : IRequest;