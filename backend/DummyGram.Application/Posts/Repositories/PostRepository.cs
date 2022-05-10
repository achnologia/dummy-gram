using DummyGram.Application.Common;
using DummyGram.Domain.Entities;
using DummyGram.Infrastructure.EFCore;

namespace DummyGram.Application.Posts.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context)
        :base(context)
    { }
}