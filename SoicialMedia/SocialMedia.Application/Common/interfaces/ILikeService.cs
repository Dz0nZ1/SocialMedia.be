using SocialMedia.Application.Common.Dto.Like;

namespace SocialMedia.Application.Common.interfaces;

public interface ILikeService
{
    Task<List<LikeDetailsDto>> GetAllAsync();

    Task<LikeDetailsDto?> GetAsync(string id);

    Task<LikeDetailsDto> CreateAsync();
}