namespace SocialMedia.Application.Common.Dto.Auth;

public record CompleteLoginResponseDto(string? EmailAddress = null, List<string>? Roles = null, string? AccessToken = null, string? RefreshToken = null);