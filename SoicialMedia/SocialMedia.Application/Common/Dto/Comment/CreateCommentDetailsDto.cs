namespace SocialMedia.Application.Common.Dto.Comment;

public record CreateCommentDetailsDto(string UserId, Guid PostId, string Content);