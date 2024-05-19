using System.Net;

namespace SocialMedia.Application.Common.Dto.Common;

public record DeleteResponseDto(bool Success, HttpStatusCode StatusCode, string Message, DateTime TimeStamp);