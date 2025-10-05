using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class ItemConfigurations : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.HasIndex(x => x.name).IsUnique(true);
            builder.Property(x => x.description);
            builder.Property(x => x.price);
            builder.Property(x => x.image);
            builder.Property(x => x.ingrediants);
            builder.HasMany<Meal>().WithMany().UsingEntity<mealDetials>();
            builder.HasMany<Order>().WithMany().UsingEntity<OrderItemDetails>();
            builder.HasMany<Ingrediant>().WithMany().UsingEntity<IngrediantItem>();
        }
    }
}
