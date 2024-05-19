using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Common.interfaces;

public interface IPostService
{
    Task<PostDetailsDto> CreateAsync(CreatePostDetailsDto post, CancellationToken cancellationToken);
}