using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities.Orders;

namespace Store.Persistance.Data.Configurations;

public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(d => d.ShortName).HasColumnType("VarChar")
            .HasMaxLength(128);
        builder.Property(d => d.Description).HasColumnType("VarChar")
            .HasMaxLength(256);
        builder.Property(d => d.DeliveryTime).HasColumnType("VarChar")
            .HasMaxLength(128);
        builder.Property(d => d.Price).HasColumnType("decimal(18,2)");
    }

}
