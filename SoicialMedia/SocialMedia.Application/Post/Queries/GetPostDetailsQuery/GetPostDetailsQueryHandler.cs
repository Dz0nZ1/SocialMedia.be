using MediatR;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Post.Queries.GetPostDetailsQuery;

public class GetPostDetailsQueryHandler(IPostService postService) : IRequestHandler<GetPostDetailsQuery, PostDetailsDto?>
{
    public async Task<PostDetailsDto?> Handle(GetPostDetailsQuery request, CancellationToken cancellationToken)
    {
        return await postService.GetAsync(request.Id);
    }
}