using DummyGram.API.Contracts.Shared;

namespace DummyGram.API.Contracts.Responses.AppUsers;

public record GetUserProfileResponse(AuthorDto user, string displayName, int postsCount, decimal subscriptionsCount, decimal subscribersCount, IEnumerable<PostMinimalDto> posts);