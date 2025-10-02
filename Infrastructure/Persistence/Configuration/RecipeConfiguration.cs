using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PrepTimeMinutes).IsRequired();
            builder.Property(x => x.CookTimeMinutes).IsRequired();
            builder.Property(x => x.Difficulty).IsRequired();
            builder.Property(x => x.Cuisine).HasMaxLength(50).IsRequired();
            builder.Property(x => x.CaloriesPerServing).IsRequired();
        }
    }
}
