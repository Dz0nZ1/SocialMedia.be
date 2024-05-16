using SocialMedia.Application.Common.Exceptions;

namespace SocialMedia.Infrastructure.Exceptions;

public class InfrastructureException(string message, object? additionalData = null)
    : BaseException(message, additionalData);