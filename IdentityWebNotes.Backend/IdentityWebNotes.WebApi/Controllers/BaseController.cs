using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebNotes.WebApi.Controllers;

// Base for all controllers
// Contains mediator for sending cqrs commands/queries 
// Attributes if we forgot set it in derived class
[ApiController]
[Route("api/[controller]/[action]")] 
public class BaseController : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    // Generates user id from his name from income JWT token
    internal Guid UserId => !User.Identity.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
}