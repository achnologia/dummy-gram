using DummyGram.API.Contracts.Requests.Story;
using DummyGram.API.Extensions;
using DummyGram.Application.Story.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyGram.API.Controllers;

[Authorize]
[Route("api/stories")]
public class StoriesController : ControllerBase
{
    private readonly IStoryService _service;

    public StoriesController(IStoryService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStoryRequest request)
    {
        var imageUrl = request.ImageUrl;
        var idUser = HttpContext.GetIdUser();
        
        var idCreated = await _service.CreateAsync(idUser, imageUrl);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/api/posts/{idCreated}";

        return Created(locationUrl, request);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var isAuthor = await _service.IsUserStoryAuthor(id, HttpContext.GetIdUser());

        if (!isAuthor)
        {
            return BadRequest(new { error = "You are not the author of the post." });
        }

        var deleted = await _service.DeleteAsync(id);

        if(!deleted)
            return NotFound();

        return NoContent();
    }
}