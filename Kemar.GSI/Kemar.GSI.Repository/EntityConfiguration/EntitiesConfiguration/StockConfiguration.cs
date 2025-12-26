using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.EntityConfiguration.BaseEntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.GSI.Repository.EntityConfiguration.EntitiesConfiguration
{
    public class StockConfiguration : BaseEntityConfiguration<Stock>
    {
        public override void Configure(EntityTypeBuilder<Stock> builder)
        {
            base.Configure(builder);

            builder.Property(s => s.StockId).ValueGeneratedOnAdd();

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.PurchasePrice).HasColumnType("decimal(18,2)");

            builder.Property(x => x.StockDate).IsRequired();
            builder.Property(x => x.RemainingQuantity).IsRequired();

            builder.HasOne(x => x.Product)
                   .WithMany(x => x.StockEntries)
                   .HasForeignKey(x => x.ProductId);
        }
    }
}
   