using DummyGram.Domain.Entities;
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

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
            .HasMany(c => c.Subscriptions)
            .WithMany(s => s.Subscribers)
            .UsingEntity(j =>
            {
                j.ToTable("Subscriptions");
            });

        modelBuilder.Entity<AppUser>()
            .HasMany(c => c.SavedPosts)
            .WithMany(s => s.SavedBy)
            .UsingEntity(j =>
            {
                j.ToTable("SavedPosts");
            });
        
        modelBuilder.Entity<PostLike>().HasKey(x => new { x.IdPost, x.IdUser });
        
        base.OnModelCreating(modelBuilder);
    }
}