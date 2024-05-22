using MediatR;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Post.Commands.DeletePostDetailsCommand;

public class DeletePostDetailsCommandHandler(IPostService postService) : IRequestHandler<DeletePostDetailsCommand, DeleteResponseDto>
{
    public async Task<DeleteResponseDto> Handle(DeletePostDetailsCommand request, CancellationToken cancellationToken)
    {
        return await postService.DeleteAsync(request.Id, cancellationToken);
    }
}