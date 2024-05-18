using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.Api.Controllers;

[ApiController]
[Route("/api/v1/[controller]/[action]")]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}