using CleanArchitecture.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts_Post");
        
        builder.Property(post => post.Code)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(post => post.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.HasOne(post => post.Category)
            .WithMany()
            .HasForeignKey(post => post.CategoryId)
            .IsRequired();
        
        builder.HasOne(post => post.Author)
            .WithMany()
            .HasForeignKey(post => post.AuthorUserId)
            .IsRequired();
    }
}