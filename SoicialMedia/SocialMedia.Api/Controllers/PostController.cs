using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Dto.Post;
using SocialMedia.Application.Post.Commands.CreatePostDetailsCommand;
using SocialMedia.Application.Post.Commands.DeletePostDetailsCommand;
using SocialMedia.Application.Post.Commands.UpdatePostDetailsCommand;
using SocialMedia.Application.Post.Queries.GetAllPostDetailsQuery;
using SocialMedia.Application.Post.Queries.GetPostDetailsQuery;

namespace SocialMedia.Api.Controllers;

public class PostController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<PostDetailsDto>>> GetAllPosts([FromQuery] GetAllPostDetailsQuery query) =>
        Ok(await Mediator.Send(query));

    [HttpGet]
    public async Task<ActionResult<PostDetailsDto>> GetPost([FromQuery] GetPostDetailsQuery query) =>
        Ok(await Mediator.Send(query));
    

    [HttpPost]
    public async Task<ActionResult<PostDetailsDto>> CreatePost(CreatePostDetailsCommand command) =>
        Ok(await Mediator.Send(command));

    [HttpPut]
    public async Task<ActionResult<PostDetailsDto?>> UpdatePost(UpdatePostDetailsCommand command) =>
        Ok(await Mediator.Send(command));

    [HttpDelete]
    public async Task<ActionResult<DeleteResponseDto>> DeletePost([FromQuery] DeletePostDetailsCommand command) =>
        Ok(await Mediator.Send(command));

}