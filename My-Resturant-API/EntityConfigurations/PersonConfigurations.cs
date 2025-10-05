using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Resturant.Entities;

namespace My_Resturant.EntityConfigurations
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).UseIdentityColumn();
            builder.Property(x => x.isActive).HasDefaultValue(true);
            builder.Property(x => x.creationDate).HasDefaultValueSql("getdate()");
            builder.Property(x => x.role).HasDefaultValueSql("12");
            builder.HasIndex(x => x.email).IsUnique(true);
            builder.HasIndex(x => x.password).IsUnique(true);
            builder.Property(x => x.firstName).HasMaxLength(20);
            builder.Property(x => x.lastName).HasMaxLength(20);
            builder.Property(x => x.passwordResetCode);
            builder.Property(x => x.passwordResetExpiry);
            builder.ToTable(x => x.HasCheckConstraint("CH_password_Length", "LEN(password) >= 5"));
            builder.Property(x => x.phone).HasMaxLength(10);
            builder.HasMany<Order>().WithOne().HasForeignKey(x => x.costumerId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<Reservation>().WithOne().HasForeignKey(x => x.customerId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
