using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.User.Commands.CreateUserCommand;
using SocialMedia.Application.User.Commands.DeleteUserCommand;
using SocialMedia.Application.User.Commands.UpdateUserCommand;
using SocialMedia.Application.User.Queries.GetAllUserDetailsQuery;
using SocialMedia.Application.User.Queries.GetUserDetailsQuery;

namespace SocialMedia.Api.Controllers;

public class UserController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<UserDetailsDto>> GetUserDetails([FromQuery] GetUserDetailsQuery query) =>
        Ok(await Mediator.Send(query));

    [HttpGet]
    public async Task<ActionResult<List<UserDetailsDto>>> GetAllUserDetails([FromQuery] GetAllUserDetailsQuery query) =>
        Ok(await Mediator.Send(query));
    
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = AuthorizationConstants.Administrator)]
    [HttpPost]
    public async Task<ActionResult> CreateUser(CreateUserCommand command) => Ok(await Mediator.Send(command));

    [HttpPut]
    public async Task<ActionResult<UserDetailsDto>> UpdateUser(UpdateUserCommand command) =>
        Ok(await Mediator.Send(command));
    
    [HttpDelete]
    public async Task<ActionResult<DeleteResponseDto>> DeleteUser(DeleteUserCommand command) => Ok(await Mediator.Send(command));
}