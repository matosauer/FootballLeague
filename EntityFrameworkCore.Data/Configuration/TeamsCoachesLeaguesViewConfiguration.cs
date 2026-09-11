using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configuration
{
    public class TeamsCoachesLeaguesViewConfiguration : IEntityTypeConfiguration<TeamsCoachesLeaguesView>
    {
        public void Configure(EntityTypeBuilder<TeamsCoachesLeaguesView> builder)
        {
            builder.HasNoKey().ToView("TeamsCoachesLeagues");
        }
    }
}
