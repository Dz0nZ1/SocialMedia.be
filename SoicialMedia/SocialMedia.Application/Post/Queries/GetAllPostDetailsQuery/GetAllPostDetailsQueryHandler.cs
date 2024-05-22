using MediatR;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Post.Queries.GetAllPostDetailsQuery;

public class GetAllPostDetailsQueryHandler(IPostService postService) : IRequestHandler<GetAllPostDetailsQuery, List<PostDetailsDto>>
{
    public async Task<List<PostDetailsDto>> Handle(GetAllPostDetailsQuery request, CancellationToken cancellationToken)
    {
        return await postService.GetAllAsync();
    }
}