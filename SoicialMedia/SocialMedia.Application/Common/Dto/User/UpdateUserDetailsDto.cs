namespace SocialMedia.Application.Common.Dto.User;

public record UpdateUserDetailsDto(string UserId, string FirstName, string LastName, string Username, string ProfilePictureUrl, string Bio);