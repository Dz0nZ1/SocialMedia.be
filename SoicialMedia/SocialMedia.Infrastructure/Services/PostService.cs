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
}