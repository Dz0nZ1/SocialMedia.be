using MediatR;
using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Post.Commands.UpdatePostDetailsCommand;

public record UpdatePostDetailsCommand(UpdatePostDetailsDto Post)  : IRequest<PostDetailsDto?>;