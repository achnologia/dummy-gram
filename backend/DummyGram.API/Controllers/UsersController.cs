using DummyGram.API.Contracts.Requests.AppUsers;
using DummyGram.API.Extensions;
using DummyGram.Application.AppUsers.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyGram.API.Controllers;

[Authorize]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IAppUserService _service;

    public UsersController(IAppUserService service)
    {
        _service = service;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateAppUserRequest request)
    {
        if (id != HttpContext.GetIdUser())
        {
            return BadRequest(new { error = "You cannot modify another user." });
        }
        
        var displayName = request.DisplayName;
        var updated = await _service.UpdateAsync(id, displayName);

        if(!updated)
            return NotFound();

        return Ok();
    }

    [HttpPost("{id}/subscriptions")]
    public async Task<IActionResult> Subscribe([FromRoute] string id, [FromBody] SubscribeToAppUserRequest request)
    {
        if (id != HttpContext.GetIdUser())
        {
            return BadRequest(new { error = "You cannot modify another user." });
        }
        
        var idSubscribeTo = request.IdSubscribeTo;
        var subscribed = await _service.SubscribeAsync(id, idSubscribeTo);
        
        if(!subscribed)
            return BadRequest();

        return Ok();
    }
    
    [HttpDelete("{id}/subscriptions/{idSubscibedTo}")]
    public async Task<IActionResult> Subscribe([FromRoute] string id, [FromRoute] string idSubscibedTo)
    {
        if (id != HttpContext.GetIdUser())
        {
            return BadRequest(new { error = "You cannot modify another user." });
        }
        
        var unsubscribed = await _service.UnsubscribeAsync(id, idSubscibedTo);
        
        if(!unsubscribed)
            return NotFound();

        return Ok();
    }
}