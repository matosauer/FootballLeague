using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configuration
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasIndex(h => new { h.Name });
            builder.HasData(
                new League { Id = 1, Name = "Premier League" },
                new League { Id = 2, Name = "La Liga" },
                new League { Id = 3, Name = "Bundesliga" },
                new League { Id = 4, Name = "Serie A" }
            );

        }
    }
}
