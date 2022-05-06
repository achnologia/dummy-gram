using DummyGram.Domain.Post;
using DummyGram.Domain.Story;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DummyGram.Infrastructure.EFCore;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    { }
    
    public DbSet<Post> Posts => Set<Post>();

    public DbSet<Story> Stories => Set<Story>();
}