using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.Common.Dto.Comment;

public record CommentDetailsDto(string Content, DateTime CreatedAt, DateTime ModifiedAt, CommentInfo Info)
{
    internal CommentDetailsDto AddCommentInfo(CommentInfo commentInfo)
    {
        return this with { Info = commentInfo };
    }
}