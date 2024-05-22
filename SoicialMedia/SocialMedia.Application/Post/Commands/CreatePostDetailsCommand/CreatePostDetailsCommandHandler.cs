using Ardalis.GuardClauses;
using MediatR;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.Post;
using NotFoundException = SocialMedia.Application.Common.Exceptions.NotFoundException;

namespace SocialMedia.Application.Post.Commands.CreatePostDetailsCommand;

public class CreatePostDetailsCommandHandler(IPostService postService) : IRequestHandler<CreatePostDetailsCommand, PostDetailsDto?>
{
    public async Task<PostDetailsDto?> Handle(CreatePostDetailsCommand request, CancellationToken cancellationToken)
    {
        return await postService.CreateAsync(request.Post, cancellationToken);
    }
}