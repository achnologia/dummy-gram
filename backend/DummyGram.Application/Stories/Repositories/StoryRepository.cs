using DummyGram.Application.Common;
using DummyGram.Domain.Entities;
using DummyGram.Infrastructure.EFCore;

namespace DummyGram.Application.Stories.Repositories;

public class StoryRepository : Repository<Story>, IStoryRepository
{
    public StoryRepository(ApplicationDbContext context) : base(context)
    { }
}