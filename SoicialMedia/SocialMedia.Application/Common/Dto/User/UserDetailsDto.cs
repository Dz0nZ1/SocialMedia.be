using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Common.Dto.User;

public record UserDetailsDto(string FirstName, string LastName, string Username, string ProfilePictureUrl, string Bio, List<PostDetailsDto> Posts);