using DummyGram.API.Contracts.Requests.AppUsers;
using DummyGram.API.Contracts.Responses.AppUsers;
using DummyGram.API.Contracts.Shared;
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
    
    [HttpPost("{id}/saved-posts")]
    public async Task<IActionResult> SavePost([FromRoute] string id, [FromBody] SavePostRequest request)
    {
        if (id != HttpContext.GetIdUser())
        {
            return BadRequest(new { error = "You cannot modify another user." });
        }
        
        var idPost = request.IdPost;
        var saved = await _service.SavePostAsync(id, idPost);
        
        if(!saved)
            return BadRequest();

        return Ok();
    }
    
    [HttpDelete("{id}/saved-posts/{idPost}")]
    public async Task<IActionResult> Subscribe([FromRoute] string id, [FromRoute] int idPost)
    {
        if (id != HttpContext.GetIdUser())
        {
            return BadRequest(new { error = "You cannot modify another user." });
        }
        
        var removed = await _service.RemoveSavedPostAsync(id, idPost);
        
        if(!removed)
            return NotFound();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserProfile([FromRoute] string id)
    {
        var user = await _service.GetUser(id);

        if (user is null)
            return NotFound();

        var userDto = new AuthorDto(user.Id, user.UserName);
        var response = new GetUserProfileResponse(userDto,
            user.DisplayName, 
            user.Posts.Count(),
            user.Subscriptions.Count(), 
            user.Subscribers.Count(), 
            user.Posts.Select(x => new PostMinimalDto(x.Id, x.ImageUrl)));

        return Ok(response);
    }
    
    [HttpGet("{id}/subscriptions")]
    public async Task<IActionResult> GetUserSubscriptions([FromRoute] string id)
    {
        var user = await _service.GetUser(id);

        if (user is null)
            return NotFound();

        var response = new GetUserSubscriptionsResponse(user.Subscriptions.Select(x => new AuthorDto(x.Id, x.UserName)));

        return Ok(response);
    }
    
    [HttpGet("{id}/subscribers")]
    public async Task<IActionResult> GetUserSubscribers([FromRoute] string id)
    {
        var user = await _service.GetUser(id);

        if (user is null)
            return NotFound();
        
        var response = new GetUserSubscribersResponse(user.Subscribers.Select(x => new AuthorDto(x.Id, x.UserName)));

        return Ok(response);
    }
    
    [HttpGet("{id}/stories")]
    public async Task<IActionResult> GetUserStories([FromRoute] string id)
    {
        var user = await _service.GetUser(id);

        if (user is null)
            return NotFound();

        var response = new GetUserStoriesResponse(user.Stories.Where(x => x.DateOfExpiration > DateTime.UtcNow).Select(x => new StoryDto(x.Id, x.ImageUrl, x.DatePosted)));

        return Ok(response);
    }
    
    [HttpGet("{id}/saved-posts")]
    public async Task<IActionResult> GetUserSavedPosts([FromRoute] string id)
    {
        var user = await _service.GetUser(id);

        if (user is null)
            return NotFound();

        var response = new GetUserSavedPosts(user.SavedPosts.Select(x => new PostMinimalDto(x.Id, x.ImageUrl)));

        return Ok(response);
    }

    [HttpGet("{id}/home-layout")]
    public async Task<IActionResult> GetUserHomeEntities([FromRoute] string id)
    {
        var user = await _service.GetUser(id);

        if (user is null)
            return NotFound();
        
        var subscriptions = user.Subscriptions;
        var posts = subscriptions.SelectMany(x => x.Posts).OrderBy(x => x.DatePosted).Select(x => 
            new PostFullDto(new AuthorDto(x.Author.Id, x.Author.UserName),
                x.ImageUrl,
                x.Description,
                x.Likes.Count(),
                x.Comments.Count(),
                x.Comments.Select(x => new CommentDto(new AuthorDto(x.IdUser, x.Author.UserName), x.Comment))));
        var stories = subscriptions.SelectMany(x => x.Stories).Where(x => x.DateOfExpiration > DateTime.UtcNow).Select(x => new AuthorDto(x.Author.Id, x.Author.UserName));
        var response = new GetUserHomeEntitiesResponse(posts, stories);

        return Ok(response);
    }
}