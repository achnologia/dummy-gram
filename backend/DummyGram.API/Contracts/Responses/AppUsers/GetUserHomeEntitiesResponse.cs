using DummyGram.API.Contracts.Shared;

namespace DummyGram.API.Contracts.Responses.AppUsers;

public record GetUserHomeEntitiesResponse(IEnumerable<PostFullDto> posts, IEnumerable<AuthorDto> stories);