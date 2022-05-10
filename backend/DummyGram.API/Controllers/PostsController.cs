using DummyGram.API.Contracts.Requests.Post;
using DummyGram.API.Extensions;
using DummyGram.Application.Posts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyGram.API.Controllers;

[Authorize]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
    {
        var (imageUrl, description) = request;
        var idUser = HttpContext.GetIdUser();
        
        var idCreated = await _postService.CreateAsync(idUser, imageUrl, description);

        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/api/posts/{idCreated}";

        return Created(locationUrl, request);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostRequest request)
    {
        var isAuthor = await _postService.IsUserPostAuthorAsync(id, HttpContext.GetIdUser());

        if (!isAuthor)
        {
            return BadRequest(new { error = "You are not the author of the post." });
        }

        var (imageUrl, description) = request;
        var updated = await _postService.UpdateAsync(id, imageUrl, description);

        if(!updated)
            return NotFound();

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var isAuthor = await _postService.IsUserPostAuthorAsync(id, HttpContext.GetIdUser());

        if (!isAuthor)
        {
            return BadRequest(new { error = "You are not the author of the post." });
        }

        var deleted = await _postService.DeleteAsync(id);

        if(!deleted)
            return NotFound();

        return NoContent();
    }
    
    [HttpPost("{id}/comments")]
    public async Task<IActionResult> AddComment([FromRoute] int id, [FromBody] AddPostCommentRequest request)
    {
        var comment = request.Comment;
        var updated = await _postService.CommentAsync(id, HttpContext.GetIdUser(), comment);

        if(!updated)
            return NotFound();

        return Ok();
    }
    
    [HttpDelete("{id}/comments/{idPostComment}")]
    public async Task<IActionResult> RemoveComment([FromRoute] int id, [FromRoute] int idPostComment)
    {
        var isPostAuthor = await _postService.IsUserPostAuthorAsync(id, HttpContext.GetIdUser());
        var isCommentAuthor = await _postService.IsUserPostCommentAuthorAsync(id, idPostComment, HttpContext.GetIdUser());

        if (!(isPostAuthor || isCommentAuthor))
        {
            return BadRequest(new { error = "You are not the author neither of the post nor the comment." });
        }

        var updated = await _postService.RemoveCommentAsync(id, idPostComment);

        if(!updated)
            return NotFound();

        return Ok();
    }
    
    [HttpPost("{id}/likes")]
    public async Task<IActionResult> AddLike([FromRoute] int id)
    {
        var updated = await _postService.LikeAsync(id, HttpContext.GetIdUser());

        if(!updated)
            return NotFound();

        return Ok();
    }
    
    [HttpDelete("{id}/likes")]
    public async Task<IActionResult> RemoveLike([FromRoute] int id)
    {
        var updated = await _postService.RemoveLikeAsync(id, HttpContext.GetIdUser());

        if(!updated)
            return NotFound();

        return Ok();
    }
}