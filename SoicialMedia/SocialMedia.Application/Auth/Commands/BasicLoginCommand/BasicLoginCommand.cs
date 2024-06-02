using MediatR;
using SocialMedia.Application.Common.Dto.Auth;

namespace SocialMedia.Application.Auth.Commands.BasicLoginCommand;

public record BasicLoginCommand(BasicLoginDto User) : IRequest<CompleteLoginResponseDto>;