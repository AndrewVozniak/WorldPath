using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Places_Service.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator { get; }
    
    public BaseController([FromServices] IMediator mediator)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
}