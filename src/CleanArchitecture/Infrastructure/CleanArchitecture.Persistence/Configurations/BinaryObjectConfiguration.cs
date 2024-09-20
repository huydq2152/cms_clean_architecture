using CleanArchitecture.Domain.Consts;
using CleanArchitecture.Domain.Entities.Apps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Persistence.Configurations;

public class BinaryObjectConfiguration: IEntityTypeConfiguration<BinaryObject>
{
    public void Configure(EntityTypeBuilder<BinaryObject> builder)
    {
        builder.ToTable("AppBinaryObjects");
        
        builder.Property(binaryObject => binaryObject.Bytes)
            .HasMaxLength(AppConsts.BinaryObjectMaxSize)
            .IsRequired();
    }
}