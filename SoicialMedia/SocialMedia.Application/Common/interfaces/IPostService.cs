using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Common.interfaces;

public interface IPostService
{
    Task<PostDetailsDto> CreateAsync(CreatePostDetailsDto post, CancellationToken cancellationToken);

    Task<PostDetailsDto?> GetAsync(string id);

    Task<List<PostDetailsDto>> GetAllAsync();

    Task<PostDetailsDto?> UpdateAsync(UpdatePostDetailsDto dto, CancellationToken cancellationToken);

    Task<DeleteResponseDto> DeleteAsync(string id, CancellationToken cancellationToken);
}