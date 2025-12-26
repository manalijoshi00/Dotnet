using Kemar.GSI.Repository.Entity.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemar.GSI.Repository.EntityConfiguration.BaseEntityConfiguration
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(b => b.CreatedAt).IsRequired();

            builder.Property(b => b.CreatedBy).IsRequired(false);

            builder.Property(b => b.UpdatedAt).IsRequired(false);

            builder.Property(b => b.UpdatedBy).IsRequired(false);

            builder.Property(b => b.IsActive).IsRequired();

        }
    }
}

