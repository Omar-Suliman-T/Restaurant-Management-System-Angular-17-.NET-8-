using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class OrderItemDetailsConfigurations : IEntityTypeConfiguration<OrderItemDetails>
    {
        public void Configure(EntityTypeBuilder<OrderItemDetails> builder)
        {
            builder.ToTable("OrderItemDetails");
            builder.HasKey(x => x.id);
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.quantity);
            builder.Property(x => x.quantity);
        }
    }
}
