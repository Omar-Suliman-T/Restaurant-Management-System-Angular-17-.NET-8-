using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class MealDetialsConfigurations : IEntityTypeConfiguration<mealDetials>
    {
        public void Configure(EntityTypeBuilder<mealDetials> builder)
        {
            builder.ToTable("MealDetails");
            builder.HasKey(x => x.id);
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.quantity);
           
        }
    }
}
