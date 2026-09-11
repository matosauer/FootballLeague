using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configuration
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasIndex(h => new { h.Name, h.TeamId }).IsUnique();
            builder.HasData(
                new Coach { Id = 1, Name = "John Doe", TeamId = 1 },
                new Coach { Id = 2, Name = "Jane Smith", TeamId = 2 }
            );

        }
    }
}
