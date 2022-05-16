using DummyGram.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DummyGram.Infrastructure.EFCore.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasMany(c => c.Subscriptions)
            .WithMany(s => s.Subscribers)
            .UsingEntity(j =>
            {
                j.ToTable("Subscriptions");
            });

        builder.HasMany(c => c.SavedPosts)
            .WithMany(s => s.SavedBy)
            .UsingEntity(j =>
            {
                j.ToTable("SavedPosts");
            });

        builder.HasMany(x => x.Posts)
            .WithOne(p => p.Author);

        builder.HasMany(x => x.Stories)
            .WithOne(s => s.Author);
    }
}