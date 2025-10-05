using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.deliveryAdress);
            builder.Property(x => x.orderStatus);
            builder.Property(x => x.rating);
            builder.Property(x => x.costumerNotes);
            builder.Property(x => x.discountCode);
            builder.HasMany<Item>().WithMany().UsingEntity<OrderItemDetails>();
            builder.HasMany<Meal>().WithMany().UsingEntity<OrderMealDetiales>();
            

        }
    }
}
