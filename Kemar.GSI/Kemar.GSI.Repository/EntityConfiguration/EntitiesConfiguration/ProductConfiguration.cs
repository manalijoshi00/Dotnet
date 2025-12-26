using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.EntityConfiguration.BaseEntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.GSI.Repository.EntityConfiguration.EntitiesConfiguration
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.ProductId).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Barcode).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.CategoryId);

            builder.HasOne(x => x.Supplier)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.SupplierId);

            builder.HasMany(x => x.StockEntries)
                   .WithOne(x => x.Product)
                   .HasForeignKey(x => x.ProductId);
        }
    }
}