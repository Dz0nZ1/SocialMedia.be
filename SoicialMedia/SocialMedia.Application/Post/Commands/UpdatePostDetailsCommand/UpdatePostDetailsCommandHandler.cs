using MediatR;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Post.Commands.UpdatePostDetailsCommand;

public class UpdatePostDetailsCommandHandler(IPostService postService) : IRequestHandler<UpdatePostDetailsCommand, PostDetailsDto?>
{
    public async Task<PostDetailsDto?> Handle(UpdatePostDetailsCommand request, CancellationToken cancellationToken)
    {
        return await postService.UpdateAsync(request.Post, cancellationToken);
    }
}