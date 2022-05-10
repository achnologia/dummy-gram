using DummyGram.Application.Common;
using DummyGram.Infrastructure.EFCore;

namespace DummyGram.Application.Story.Repositories;

public class StoryRepository : Repository<Domain.Entities.Story>, IStoryRepository
{
    public StoryRepository(ApplicationDbContext context) : base(context)
    { }
}