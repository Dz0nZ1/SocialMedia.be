namespace SocialMedia.Application.Common.Dto.Auth;

public record CompleteLoginResponseDto(string Id, string FirstName, string LastName, string Username, string? EmailAddress = null, List<string>? Roles = null, string? AccessToken = null, string? RefreshToken = null);