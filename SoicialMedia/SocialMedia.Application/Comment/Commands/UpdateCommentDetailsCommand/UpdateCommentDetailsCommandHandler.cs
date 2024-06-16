using MediatR;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Comment.Commands.UpdateCommentDetailsCommand;

public class UpdateCommentDetailsCommandHandler(ICommentService commentService) : IRequestHandler<UpdateCommentDetailsCommand, CommentDetailsDto?>
{
    public async Task<CommentDetailsDto?> Handle(UpdateCommentDetailsCommand request, CancellationToken cancellationToken)
    {
        return await commentService.UpdateAsync(request.Comment, cancellationToken);
    }
}