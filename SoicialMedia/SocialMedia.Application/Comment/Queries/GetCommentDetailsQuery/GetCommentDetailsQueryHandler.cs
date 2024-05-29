using MediatR;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Comment.Queries.GetCommentDetailsCommand;

public class GetCommentDetailsQueryHandler(ICommentService commentService) : IRequestHandler<GetCommentDetailsQuery, CommentDetailsDto?>
{
    public async Task<CommentDetailsDto?> Handle(GetCommentDetailsQuery request, CancellationToken cancellationToken)
    {
        return await commentService.GetAsync(request.Id);
    }
}