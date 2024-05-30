using MediatR;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Comment.Commands.DeleteCommentDetilasCommand;

public class DeleteCommentDetailsCommandHandler(ICommentService commentService) : IRequestHandler<DeleteCommentDetailsCommand, DeleteResponseDto>
{
    public async Task<DeleteResponseDto> Handle(DeleteCommentDetailsCommand request, CancellationToken cancellationToken)
    {
        return await commentService.DeleteAsync(request.Id, cancellationToken);
    }
}