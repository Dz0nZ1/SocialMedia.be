using EMS.Application.Common.Dto.Auth;
using MediatR;

namespace SocialMedia.Application.Auth.Commands.CompleteLoginCommand;

public record CompleteLoginCommand(string ValidationToken) : IRequest<CompleteLoginResponseDto>;