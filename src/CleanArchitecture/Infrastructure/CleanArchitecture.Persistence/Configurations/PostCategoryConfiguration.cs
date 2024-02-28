using CleanArchitecture.Domain.Entities.Posts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public class PostCategoryConfiguration: IEntityTypeConfiguration<PostCategory>
{
    public void Configure(EntityTypeBuilder<PostCategory> builder)
    {
        builder.ToTable("Posts_PostCategory");
        
        builder.Property(postCategory => postCategory.Code)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(postCategory => postCategory.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasOne(postCategory => postCategory.Parent)
            .WithMany()
            .HasForeignKey(postCategory => postCategory.ParentId);
    }
}