using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Application.Auth.Commands.BasicLoginCommand;
using SocialMedia.Application.Auth.Commands.BeginLoginCommand;
using SocialMedia.Application.Auth.Commands.CompleteLoginCommand;
using SocialMedia.Application.Auth.Commands.LogoutCommand;
using SocialMedia.Application.Auth.Commands.RefreshTokenCommand;
using SocialMedia.Application.Common.Dto.Auth;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.User.Commands.RegisterUserCommand;

namespace SocialMedia.Api.Controllers;

public class AuthController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> BeginLogin(BeginLoginCommand command) => Ok(await Mediator.Send(command));

    [AllowAnonymous]
    [HttpGet("{validationToken}/CompleteLogin")]
    public async Task<ActionResult> CompleteLogin([FromRoute] string validationToken) => Ok(await Mediator.Send(new CompleteLoginCommand(validationToken)));
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login(BasicLoginCommand command) => Ok(await Mediator.Send(command));

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Logout(LogoutCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<UserDetailsDto>> Register(RegisterUserCommand command) =>
        Ok(await Mediator.Send(command));

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<TokenResponse>> Refresh(RefreshTokenCommand command) =>
        Ok(await Mediator.Send(command));



}