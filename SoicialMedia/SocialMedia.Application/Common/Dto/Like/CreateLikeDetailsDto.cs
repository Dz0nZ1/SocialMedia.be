namespace SocialMedia.Application.Common.Dto.Like;

public record CreateLikeDetailsDto(DateTime CreatedAt, string? PostId, string? CommentId, string UserId);
