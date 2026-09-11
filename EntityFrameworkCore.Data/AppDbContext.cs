using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Data
{
    /// <summary>
    /// AppDbContext belongs in Data layer, it is responsible for managing the database context and providing access to the database entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        public DbSet<Team> Teams => Set<Team>();
        public DbSet<League> Leagues => Set<League>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Coach> Coaches => Set<Coach>();
        public DbSet<TeamsCoachesLeaguesView> TeamsCoachesLeagues => Set<TeamsCoachesLeaguesView>();

        //keep EF configurations in Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new TeamConfiguration());

            //  *************************************************
            //
            //  When we want EF Core to automatically discover and apply your entity configurations instead of manually registering each one
            //  The following basically tells EF Core: 
            //  "Look through the assembly containing AppDbContext, find all classes implementing IEntityTypeConfiguration<T>, and apply their configurations."
            //
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            //
            //  *************************************************
        }
    }
}
