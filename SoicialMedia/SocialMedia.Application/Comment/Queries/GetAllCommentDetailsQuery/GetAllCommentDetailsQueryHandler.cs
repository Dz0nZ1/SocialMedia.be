using MediatR;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Comment.Queries.GetAllCommentDetailsQuery;

public class GetAllCommentDetailsQueryHandler(ICommentService commentService) : IRequestHandler<GetAllCommentDetailsQuery, List<CommentDetailsDto>>
{
    public async Task<List<CommentDetailsDto>> Handle(GetAllCommentDetailsQuery request, CancellationToken cancellationToken)
    {
        return await commentService.GetAllAsync();
    }
}