namespace DummyGram.API.Contracts.Shared;

public record PostFullDto(AuthorDto Author, string ImageUrl, string Description, decimal likeCount, decimal commentCount, IEnumerable<CommentDto> comments);