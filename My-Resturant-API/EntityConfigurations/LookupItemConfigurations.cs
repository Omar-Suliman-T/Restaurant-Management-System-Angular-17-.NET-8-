using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class LookupItemConfigurations : IEntityTypeConfiguration<LookupItem>
    {
        public void Configure(EntityTypeBuilder<LookupItem> builder)
        {
            builder.ToTable("LoolupItems");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.HasIndex(x => x.name).IsUnique(true);
            builder.HasMany<Person>().WithOne().HasForeignKey(x => x.role).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany<Order>().WithOne().HasForeignKey(x => x.orderStatus).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Order>().WithOne().HasForeignKey(x => x.rating).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Category>().WithOne().HasForeignKey(x => x.name).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany<Ingrediant>().WithOne().HasForeignKey(x => x.unit).OnDelete(DeleteBehavior.SetNull);
            builder.HasMany<Reservation>().WithOne().HasForeignKey(x => x.status).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
