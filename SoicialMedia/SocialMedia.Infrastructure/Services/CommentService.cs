using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.Comment;
using SocialMedia.Application.Common.Mappers.Post;

namespace SocialMedia.Infrastructure.Services;

public class CommentService(ISmDbContext dbContext, IPostService postService, IUserService userService) : ICommentService
{
    public Task<CommentDetailsDto?> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CommentDetailsDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<CommentDetailsDto> CreateAsync(CreateCommentDetailsDto commentDto, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserEntityAsync(commentDto.UserId) ?? throw new NotFoundException("User not found");
        var postDto = await postService.GetAsync(commentDto.PostId.ToString()) ?? throw new NotFoundException("Post not found");
        var comment = commentDto.ToEntity().AddUser(user).AddPost(postDto.ToEntity().AddUser(user)).AddCreatedAt(DateTime.Now)
            .AddModifiedAt(DateTime.Now);
        await dbContext.Comments.AddAsync(comment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return comment.ToDetailsDto();
    }

    public Task<CommentDetailsDto?> UpdateAsync(UpdateCommentDetailsDto commentDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteResponseDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}