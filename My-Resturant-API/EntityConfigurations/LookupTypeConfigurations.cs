using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class LookupTypeConfigurations : IEntityTypeConfiguration<LookupType>
    {
        public void Configure(EntityTypeBuilder<LookupType> builder)
        {
            builder.ToTable("LookupTypes");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.HasIndex(x => x.name).IsUnique(true);
            builder.HasMany<LookupItem>().WithOne().HasForeignKey(x => x.lookupTypeID).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
