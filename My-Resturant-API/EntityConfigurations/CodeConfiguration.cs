using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class CodeConfiguration : IEntityTypeConfiguration<Code>
    {
        public void Configure(EntityTypeBuilder<Code> builder)
        {
            builder.ToTable("Codes");
            builder.HasKey(x => x.id);
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.discountCode);
        }
    }
}
