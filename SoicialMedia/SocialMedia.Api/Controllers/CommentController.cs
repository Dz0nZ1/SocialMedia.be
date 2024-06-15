using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Comment.Commands.CreateCommentDetailsCommand;
using SocialMedia.Application.Comment.Commands.DeleteCommentDetilasCommand;
using SocialMedia.Application.Comment.Commands.UpdateCommentDetailsCommand;
using SocialMedia.Application.Comment.Queries.GetAllCommentDetailsQuery;
using SocialMedia.Application.Comment.Queries.GetCommentDetailsCommand;
using SocialMedia.Application.Common.Dto.Comment;
using SocialMedia.Application.Common.Dto.Common;

namespace SocialMedia.Api.Controllers;

public class CommentController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CommentDetailsDto>> GetComment([FromQuery] GetCommentDetailsQuery query) =>
        Ok(await Mediator.Send(query));

    [HttpGet]
    public async Task<ActionResult<List<CommentDetailsDto>>>
        GetAllComments([FromQuery] GetAllCommentDetailsQuery query) => Ok(await Mediator.Send(query));
    
    [HttpPost]
    public async Task<ActionResult<CommentDetailsDto>> CreateComment(CreateCommentDetailsCommand command) =>
        Ok(await Mediator.Send(command));


    [HttpPut]
    public async Task<ActionResult<CommentDetailsDto>> UpdateComment(UpdateCommentDetailsCommand command) =>
        Ok(await Mediator.Send(command));

    [HttpDelete]
    public async Task<ActionResult<CommentDetailsDto>> DeleteComment(DeleteCommentDetailsCommand command) =>
        Ok(await Mediator.Send(command));


}