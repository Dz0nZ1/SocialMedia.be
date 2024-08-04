using MediatR;
using SocialMedia.Application.Common.Dto.Like;

namespace SocialMedia.Application.Like.Commands;

public record CreateLikeDetailsCommand(CreateLikeDetailsDto Like) : IRequest<LikeDetailsDto>;