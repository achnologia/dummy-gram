using DummyGram.Domain.Post;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DummyGram.Infrastructure.EFCore;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    { }
    
    public DbSet<Post> Posts => Set<Post>();
}