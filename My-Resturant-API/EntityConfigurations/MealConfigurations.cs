using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class MealConfigurations : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.ToTable("Meals");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.description);
            builder.Property(x => x.price);
            builder.Property(x => x.stock);
            builder.Property(x => x.image);
            builder.HasMany<Item>().WithMany().UsingEntity<mealDetials>();
        }
    }
}
