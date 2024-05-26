using EMS.Application.Common.Dto.Auth;
using MediatR;
using SocialMedia.Application.Common.Dto.Auth;

namespace SocialMedia.Application.Auth.Commands.CompleteLoginCommand;

public record CompleteLoginCommand(string ValidationToken) : IRequest<CompleteLoginResponseDto>;