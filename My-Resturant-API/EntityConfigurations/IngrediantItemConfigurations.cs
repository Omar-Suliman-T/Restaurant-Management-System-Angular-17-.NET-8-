using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class IngrediantItemConfigurations : IEntityTypeConfiguration<IngrediantItem>
    {
        public void Configure(EntityTypeBuilder<IngrediantItem> builder)
        {
            builder.ToTable("IngrediantItem");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
        }
    }
}
