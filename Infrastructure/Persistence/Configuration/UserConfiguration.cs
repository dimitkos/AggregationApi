using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).IsRequired();
        }
    }
}
