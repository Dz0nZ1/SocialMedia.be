namespace SocialMedia.Application.Common.Exceptions;

public class UserAlreadyExistsException : BaseException
{
    public UserAlreadyExistsException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }

    public UserAlreadyExistsException(string message, Exception innerException, object? additionalData = null) : base(message, innerException, additionalData)
    {
    }
}