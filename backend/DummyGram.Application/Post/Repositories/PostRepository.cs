using DummyGram.Infrastructure.EFCore;
using DummyGram.Application.Common;

namespace DummyGram.Application.Post.Repositories;

public class PostRepository : Repository<Domain.Entities.Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context)
        :base(context)
    { }
}