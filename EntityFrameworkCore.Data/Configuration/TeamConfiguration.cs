using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(h => h.Name).IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(x => x.League)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.LeagueId)
                .IsRequired();

            builder.HasData(
                new Team { Id = 1, Name = "Manchester United", LeagueId = 1 },
                new Team { Id = 2, Name = "Liverpool", LeagueId = 1 },
                new Team { Id = 3, Name = "Bayern Munich", LeagueId = 2 },
                new Team { Id = 4, Name = "Borussia Dortmund", LeagueId = 2 }
            );
        }
    }
}
