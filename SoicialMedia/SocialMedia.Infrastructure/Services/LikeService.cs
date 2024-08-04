using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Dto.Like;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.Like;

namespace SocialMedia.Infrastructure.Services;

public class LikeService(ISmDbContext dbContext) : ILikeService
{
    public async Task<List<LikeDetailsDto>> GetAllAsync()
    {
        var likes = await dbContext.Likes.ToListAsync();
        return likes.ToDetailsListDto();
    }

    public async Task<LikeDetailsDto?> GetAsync(string id)
    {
        var like = await dbContext.Likes.Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefaultAsync() ?? throw new NotFoundException("Like not found");
        return like.ToDetailsDto();
    }

    public Task<LikeDetailsDto> CreateAsync()
    {
        throw new NotImplementedException();
    }
}