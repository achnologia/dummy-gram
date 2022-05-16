using DummyGram.API.Contracts.Shared;

namespace DummyGram.API.Contracts.Responses.AppUsers;

public record GetUserSavedPosts(IEnumerable<PostMinimalDto> posts);