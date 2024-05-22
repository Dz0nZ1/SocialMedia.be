using MediatR;
using SocialMedia.Application.Common.Dto.Post;

namespace SocialMedia.Application.Post.Commands.CreatePostDetailsCommand;

public record CreatePostDetailsCommand(CreatePostDetailsDto Post) : IRequest<PostDetailsDto?>;