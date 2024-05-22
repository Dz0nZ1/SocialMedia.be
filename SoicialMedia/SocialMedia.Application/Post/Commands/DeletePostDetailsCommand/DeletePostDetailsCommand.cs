using MediatR;
using SocialMedia.Application.Common.Dto.Common;

namespace SocialMedia.Application.Post.Commands.DeletePostDetailsCommand;

public record DeletePostDetailsCommand(string Id) : IRequest<DeleteResponseDto>;