using System.Net;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Constants.Post;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.Post;

namespace SocialMedia.Infrastructure.Services;

public class PostService(ISmDbContext dbContext, IUserService userService) : IPostService
{
    public async Task<PostDetailsDto> CreateAsync(CreatePostDetailsDto post, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserEntityAsync(post.UserId) ?? throw new NotFoundException("Post not found");
        
        var entity = post.ToEntity().AddUser(user).AddCreatedAt(DateTime.Now).AddModifiedAt(DateTime.Now);
        
        await dbContext.Posts.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity.ToDetailsDto();
    }

    public async Task<PostDetailsDto?> GetAsync(string id)
    {
        var post = await dbContext.Posts
            .Include(x => x.Comments)
            .ThenInclude(x => x.User)
            .Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefaultAsync() ?? throw new NotFoundException("Post not found");
        return post.ToDetailsDto();
    }

    public async Task<List<PostDetailsDto>> GetAllAsync()
    {
        var posts = await dbContext.Posts
            .Include(x => x.User)   
            .Include(x => x.Comments).ToListAsync();
        return posts.ToDetailsListDto();
    }

    public async Task<PostDetailsDto?> UpdateAsync(UpdatePostDetailsDto dto, CancellationToken cancellationToken)
    {
        var post = await dbContext.Posts.Where(x => x.Id.Equals(Guid.Parse(dto.PostId))).FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new NotFoundException("Post not found");
        post.ToEntity(dto);
        await dbContext.SaveChangesAsync(cancellationToken);
        return post.ToDetailsDto();
    }

    public async Task<DeleteResponseDto> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var post = await dbContext.Posts.Where(x => x.Id.Equals(Guid.Parse(id))).FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new NotFoundException("Post not found");
        dbContext.Posts.Remove(post);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteResponseDto(true, HttpStatusCode.OK, PostConstants.PostDeleted, DateTime.Now);
    }
}