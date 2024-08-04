using System.Net;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Constants.Comment;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.Comment;
using SocialMedia.Application.Common.Mappers.Post;

namespace SocialMedia.Infrastructure.Services;

public class CommentService(ISmDbContext dbContext, IUserService userService) : ICommentService
{
    public async Task<CommentDetailsDto?> GetAsync(string id)
    {
        var comment = await dbContext.Comments
                          .Include(x => x.User)
                          .Include(x => x.Post)
                          .Where(x => x.Id.Equals(Guid.Parse(id)))
                          .FirstOrDefaultAsync() ??
                   throw new NotFoundException("Comment not found");
        return comment.ToDetailsDto();
    }

    public async Task<List<CommentDetailsDto>> GetAllAsync()
    {
        var commentList = await dbContext.Comments
            .Include(x => x.User)
            .Include(x => x.Post)
            .ToListAsync();
        return commentList.ToDetailsListDto();
    }

    public async Task<CommentDetailsDto> CreateAsync(CreateCommentDetailsDto commentDto, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserEntityAsync(commentDto.UserId) ?? throw new NotFoundException("User not found");
        var post = await dbContext.Posts.Where(x => x.Id.Equals(commentDto.PostId)).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException("Post not found");
        var comment = commentDto.ToEntity().AddUser(user).AddPost(post).AddCreatedAt(DateTime.Now)
            .AddModifiedAt(DateTime.Now);
        await dbContext.Comments.AddAsync(comment, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return comment.ToDetailsDto();
    }

    public async Task<CommentDetailsDto?> UpdateAsync(UpdateCommentDetailsDto commentDto, CancellationToken cancellationToken)
    {
        var comment = await dbContext.Comments
                          .Include(x => x.User)
                          .Include(x => x.Post)
                          .Where(x => x.Id.Equals(Guid.Parse(commentDto.CommentId)))
                          .FirstOrDefaultAsync(cancellationToken) ??
                      throw new NotFoundException("Comment not found");
        comment.ToEntity(commentDto);
        await dbContext.SaveChangesAsync(cancellationToken);
        return comment.ToDetailsDto();
    }

    public async Task<DeleteResponseDto> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var comment = await dbContext.Comments
                          .Include(x => x.User)
                          .Include(x => x.Post)
                          .Where(x => x.Id.Equals(Guid.Parse(id)))
                          .FirstOrDefaultAsync(cancellationToken) ??
                      throw new NotFoundException("Comment not found");
        dbContext.Comments.Remove(comment);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new DeleteResponseDto(true, HttpStatusCode.OK, CommentConstants.CommentDeleted, DateTime.Now);
    }
}