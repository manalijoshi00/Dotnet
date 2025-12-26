using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.EntityConfiguration.BaseEntityConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.GSI.Repository.EntityConfiguration.EntitiesConfiguration
{
    public class SupplierConfiguration : BaseEntityConfiguration<Supplier>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.SupplierId).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ContactPerson).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Mobile).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).HasMaxLength(120);
            builder.Property(x => x.Address).HasMaxLength(250);

            builder.HasMany(x => x.Products)
                   .WithOne(x => x.Supplier)
                   .HasForeignKey(x => x.SupplierId);

        }
    }
}
