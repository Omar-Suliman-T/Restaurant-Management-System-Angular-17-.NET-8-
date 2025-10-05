using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurationszz
{
    public class IngrediantConfigurations : IEntityTypeConfiguration<Ingrediant>
    {
        public void Configure(EntityTypeBuilder<Ingrediant> builder)
        {
            builder.ToTable("Ingrediants");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.image);
            builder.Property(x => x.name);
            builder.Property(x => x.unit);
            builder.HasMany<Item>().WithMany().UsingEntity<IngrediantItem>();                                                                                                                                                                                                                                                                       
        }
    }
}
           