using DummyGram.API.Contracts.Shared;

namespace DummyGram.API.Contracts.Responses.Posts;

public record GetPostDetailsResponse(AuthorDto Author, string ImageUrl, string Description, decimal likeCount, decimal commentCount, List<CommentDto> comments);