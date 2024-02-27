using CleanArchitecture.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public class PostCategoryConfiguration: IEntityTypeConfiguration<PostCategory>
{
    public void Configure(EntityTypeBuilder<PostCategory> builder)
    {
        builder.Property(t => t.Code)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();
    }
}