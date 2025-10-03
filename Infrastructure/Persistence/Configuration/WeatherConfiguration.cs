using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    class WeatherConfiguration : IEntityTypeConfiguration<Weather>
    {
        public void Configure(EntityTypeBuilder<Weather> builder)
        {
            builder.ToTable("Weather");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Temp).IsRequired();
            builder.Property(x => x.Humidity).IsRequired();
        }
    }
}
