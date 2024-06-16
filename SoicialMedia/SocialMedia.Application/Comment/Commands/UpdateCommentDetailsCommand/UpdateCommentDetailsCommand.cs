using MediatR;
using SocialMedia.Application.Common.Dto.Comment;

namespace SocialMedia.Application.Comment.Commands.UpdateCommentDetailsCommand;

public record UpdateCommentDetailsCommand(UpdateCommentDetailsDto Comment) : IRequest<CommentDetailsDto?>;