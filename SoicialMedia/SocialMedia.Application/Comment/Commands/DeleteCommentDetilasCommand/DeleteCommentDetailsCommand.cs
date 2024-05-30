using MediatR;
using SocialMedia.Application.Common.Dto.Common;

namespace SocialMedia.Application.Comment.Commands.DeleteCommentDetilasCommand;

public record DeleteCommentDetailsCommand(string Id) : IRequest<DeleteResponseDto>;