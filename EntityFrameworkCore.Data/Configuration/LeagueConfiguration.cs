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
                new League { Id = 20, Name = "Sample League" },
                new League { Id = 21, Name = "Sample League 2" }
            );

        }
    }
}
