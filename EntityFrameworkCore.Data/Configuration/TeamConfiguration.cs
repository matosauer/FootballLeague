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
                new Team { Id = 20, Name = "Team A", LeagueId = 20 },
                new Team { Id = 21, Name = "Team B", LeagueId = 20 },
                new Team { Id = 22, Name = "Team C", LeagueId = 21 },
                new Team { Id = 23, Name = "Team D", LeagueId = 21 }
            );
        }
    }
}
