using MediatR;
using SocialMedia.Application.Common.Dto.Comment;

namespace SocialMedia.Application.Comment.Commands.CreateCommentDetailsCommand;

public record CreateCommentDetailsCommand(CreateCommentDetailsDto Comment) : IRequest<CommentDetailsDto>
{
    
}