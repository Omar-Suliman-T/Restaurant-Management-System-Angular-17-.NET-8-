using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.id);
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.HasIndex(x => x.name).IsUnique(true);
            builder.Property(x => x.description);
            builder.HasMany<Item>().WithOne().HasForeignKey(x => x.category).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany<Meal>().WithOne().HasForeignKey(x => x.category).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
