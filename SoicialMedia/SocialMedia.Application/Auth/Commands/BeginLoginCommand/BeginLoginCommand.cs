using EMS.Application.Common.Dto.Auth;
using MediatR;

namespace SocialMedia.Application.Auth.Commands.BeginLoginCommand;

public record BeginLoginCommand(string EmailAddress) : IRequest<BeginLoginResponseDto>;
