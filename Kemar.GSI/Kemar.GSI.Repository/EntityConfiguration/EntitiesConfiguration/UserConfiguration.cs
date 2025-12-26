using Kemar.GSI.Repository.Entity.Entities;
using Kemar.GSI.Repository.EntityConfiguration.BaseEntityConfiguration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Kemar.GSI.Repository.EntityConfiguration.EntitiesConfiguration
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.UserId).ValueGeneratedOnAdd();

            builder.Property(u => u.Username).IsRequired().HasMaxLength(100);

            builder.Property(u =>u.PasswordHash).IsRequired().HasMaxLength(255);

            builder.Property(u => u.Role).IsRequired().HasMaxLength(50).HasDefaultValue("User");

            builder.HasIndex(u => u.Username).IsUnique();
        }
    }
}