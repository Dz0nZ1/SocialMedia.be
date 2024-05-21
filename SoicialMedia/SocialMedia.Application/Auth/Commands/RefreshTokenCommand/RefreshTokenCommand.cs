using MediatR;
using SocialMedia.Application.Common.Dto.Auth;

namespace SocialMedia.Application.Auth.Commands.RefreshTokenCommand;

public record RefreshTokenCommand(TokenRequest Request) : IRequest<TokenResponse>;