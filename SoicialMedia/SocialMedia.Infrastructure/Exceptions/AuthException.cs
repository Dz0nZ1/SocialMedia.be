using SocialMedia.Application.Common.Exceptions;

namespace SocialMedia.Infrastructure.Exceptions;

public class AuthException(string message, object? additionalData = null) : BaseException(message, additionalData){}