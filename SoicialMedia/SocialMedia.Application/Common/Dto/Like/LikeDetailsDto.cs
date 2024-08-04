using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.Dto.User;

namespace SocialMedia.Application.Common.Dto.Like;

public record LikeDetailsDto(PostDetailsDto? Post, CommentDetailsDto? Comment, UserFullNameDto UserFullName, bool isLocked);