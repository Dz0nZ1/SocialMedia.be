using MediatR;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Comment.Commands.CreateCommentDetailsCommand;

public class CreateCommentDetailsCommandHandler(ICommentService commentService) : IRequestHandler<CreateCommentDetailsCommand, CommentDetailsDto>
{
    public async Task<CommentDetailsDto> Handle(CreateCommentDetailsCommand request, CancellationToken cancellationToken)
    {
        return await commentService.CreateAsync(request.Comment, cancellationToken);
    }
}