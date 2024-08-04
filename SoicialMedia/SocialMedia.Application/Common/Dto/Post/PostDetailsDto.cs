using SocialMedia.Application.Common.Dto.Comment;

namespace SocialMedia.Application.Common.Dto.Post;

public record PostDetailsDto(string Id, string Content, string ImageUrl, List<CommentDetailsDto> Comments);