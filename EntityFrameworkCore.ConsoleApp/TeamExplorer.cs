using EntityFrameworkCore.Data;
using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.ConsoleApp
{
    public class TeamExplorer
    {
        private readonly AppDbContext context;

        public TeamExplorer(AppDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (await context.Leagues.AnyAsync() || await context.Teams.AnyAsync())
            {
                return; // Data already seeded
            }

            //1
            var premierLeague = new League { Name = "Premier League" };
            await context.Leagues.AddAsync(premierLeague);

            var teams = new[]
                {
                    new Team { Name = "Manchester United", League = premierLeague },
                    new Team { Name = "Liverpool", League = premierLeague }
                };

            await context.Teams.AddRangeAsync(teams);

            //2
            var bundesliga = new League { Name = "Bundesliga" };
            await context.Leagues.AddAsync(bundesliga);

            var teams2 = new[]
                {
                    new Team { Name = "Bayern Munich", League = bundesliga },
                    new Team { Name = "Borussia Dortmund", League = bundesliga }
                };

            await context.Teams.AddRangeAsync(teams2);

            await context.SaveChangesAsync();
        }

        public async Task<List<Team>> GetAllTeamsAsync()
        {
            return await context
                .Teams
                .Include(t => t.League)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task PrintAllTeamsAsync()
        {
            var teams = await GetAllTeamsAsync();
            foreach (var team in teams)
            {
                Console.WriteLine($"Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }
        }

        public async Task PrintTeamsByLeagueAsync(string leagueName)
        {
            var teams = await context
                .Teams
                .Include(t => t.League)
                .Where(t => t.League != null && t.League.Name == leagueName)
                .AsNoTracking()
                .ToListAsync();

            foreach (var team in teams)
            {
                Console.WriteLine($"Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }
        }

        public async Task PrintNPlusOne()
        {
            var teams = await context.Teams.ToListAsync();
            foreach (var team in teams)
            {
                Console.WriteLine($"Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }
        }

        public async Task QueryFiltersAsync()
        {
            var teams = await context
                .Teams
                .Include(t => t.League)
                .Where(t => t.League != null && t.League.Name.Equals("Premier leaguE"))
                .ToListAsync();

            foreach (var team in teams)
            {
                Console.WriteLine($"Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }

            teams = await context
                .Teams
                .Include(t => t.League)
                .Where(t => EF.Functions.Like(t.Name, "%man%"))
                .ToListAsync();

            foreach (var team in teams)
            {
                Console.WriteLine($"Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }
        }

        public async Task AdditionalExecutionAsync()
        {
            //var teams = await context
            //    .Teams
            //    .Include(t => t.League)
            //    .Where(t => t.Name.Contains("man"))
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync();

            var teams = await context
                .Teams
                .Include(t => t.League)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Name.Contains("man"));

            if (teams != null)
            {
                Console.WriteLine($"Team ID: {teams.Id}, Team Name: {teams.Name}, League: {teams.League?.Name}");
            }
        }

        public async Task AdditionalExecutionMethodsAsync()
        {
            //// These methods also have non-async
            var leagues = context.Leagues;
            var list = await leagues.ToListAsync();
            var first = await leagues.FirstAsync();
            var firstOrDefault = await leagues.FirstOrDefaultAsync();
            //var single = await leagues.SingleAsync();
            //var singleOrDefault = await leagues.SingleOrDefaultAsync();

            var count = await leagues.CountAsync();
            var longCount = await leagues.LongCountAsync();
            var min = await leagues.MinAsync(t => t.Id);
            var max = await leagues.MaxAsync(t => t.Id);

            //// DbSet Method that will execute
            var league = await leagues.FindAsync(1);

        }

        public async Task AddPremierTeamAsync()
        {
            var league = await context.Leagues.FirstOrDefaultAsync(l => l.Name == "Premier League");
            if (league != null)
            {
                var newTeam = new Team
                {
                    Name = "New Premier Team",
                    League = league
                };
                await context.Teams.AddAsync(newTeam);
                await context.SaveChangesAsync();

                Console.WriteLine($"Added Team ID: {newTeam.Id}, Team Name: {newTeam.Name}, League: {newTeam.League?.Name}");
            }
        }

        public async Task UpdatePremierTeamAsync()
        {
            var team = await context.Teams.FirstOrDefaultAsync(t => t.Name == "New Premier Team");
            if (team != null)
            {
                team.Name = "Updated Premier Team";
                await context.SaveChangesAsync();
                Console.WriteLine($"Updated Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }
        }

        public async Task UpdatePremierLeagueTeamsAsync()
        {
            var teams = await context.Teams
                .Include(t => t.League)
                .Where(t => t.League != null && t.League.Name == "Premier League")
                .ToListAsync();

            foreach (var team in teams)
            {
                team.Name += " (Updated)";
            }

            await context.SaveChangesAsync();

            Console.WriteLine("Updated Premier League Teams:");
            foreach (var team in teams)
            {
                Console.WriteLine($"Team ID: {team.Id}, Team Name: {team.Name}, League: {team.League?.Name}");
            }
        }


        public async Task UpdateTeamAsync()
        {
            await context.Teams
                .Where(t => t.Id == 1)
                .ExecuteUpdateAsync(s =>
                     s.SetProperty(t => t.Name, "Manchester United")
                    );


            //update league id foreign key property
            await context.Teams
                .Where(t => t.Id == 1)
                .ExecuteUpdateAsync(s =>
                     s.SetProperty(t => t.LeagueId, 2)
                    );

            await context.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync()
        {
            var team = await context.Teams.FirstOrDefaultAsync(t => t.Name == "Borussia Dortmund");
            if (team != null)
            {
                context.Teams.Remove(team);
                await context.SaveChangesAsync();
                Console.WriteLine($"Deleted Team ID: {team.Id}, Team Name: {team.Name}");
            }

            context.Teams.RemoveRange(context.Teams.Where(t => t.League != null && t.League.Name == "Premier League"));

            await context.SaveChangesAsync();
        }

        public async Task AddNewTeamsWithLeagueAsync()
        {
            var league = new League { Name = "Bundesliga" };
            var team = new Team { Name = "Bayern Munich", League = league };
            await context.AddAsync(team);
            await context.SaveChangesAsync();
        }

        public async Task AddNewTeamWithLeagueId()
        {
            var team = new Team { Name = "Fiorentina", LeagueId = 2 };
            await context.AddAsync(team);
            await context.SaveChangesAsync();
        }

        public async Task AddNewLeagueWithTeams()
        {
            var teams = new List<Team> {
                        new Team
                        {
                            Name = "Rivoli United"
                        },
                        new Team
                        {
                            Name = "Waterhouse FC"
                        },
                    };

            var league = new League { Name = "CIFA", Teams = teams };

            await context.AddAsync(league);
            await context.SaveChangesAsync();
        }

        public async Task AddNewMatchesAsync()
        {
            var matches = new List<Match>
                    {
                        new Match
                        {
                            AwayTeamId = 1, HomeTeamId = 3, MatchDate = new DateTime(2021, 10, 28)
                        },
                        new Match
                        {
                            AwayTeamId = 3, HomeTeamId = 5, MatchDate = DateTime.Now
                        },
                        new Match
                        {
                            AwayTeamId = 6, HomeTeamId = 8, MatchDate = DateTime.Now
                        }
                    };
            await context.AddRangeAsync(matches);
            await context.SaveChangesAsync();
        }

        public async Task AddNewCoachAsync()
        {
            var coach1 = new Coach { Name = "Jose Mourinho", TeamId = 3 };
            var coach2 = new Coach { Name = "Antonio Conte" };

            await context.Coaches.AddRangeAsync(new List<Coach> { coach1, coach2 });
            await context.SaveChangesAsync();
        }

        public async Task QueryRelatedRecordsAsync()
        {
            // Get Many Related Records - Leagues -> Teams
            var leagues = await context.Leagues.Include(q => q.Teams).ToListAsync();

            // Get One Related Record - Team -> Coach
            var team = await context.Teams
                .Include(q => q.Coach)
                .FirstOrDefaultAsync(q => q.Id == 3);

            // Get 'Grand Children' Related Record - Team -> Matches -> Home/Away Team
            var teamsWithMatchesAndOpponents = await context.Teams
                .Include(q => q.AwayMatches).ThenInclude(q => q.HomeTeam).ThenInclude(q => q.Coach)
                .Include(q => q.HomeMatches).ThenInclude(q => q.AwayTeam).ThenInclude(q => q.Coach)
                .FirstOrDefaultAsync(q => q.Id == 1);

            // Get Includes with filters
            var teams = await context.Teams
                .Where(q => q.HomeMatches.Count > 0)
                .Include(q => q.Coach)
                .ToListAsync();

            var teamsagain = await context.Teams
                .Include(q => q.HomeMatches)
                .Select(
                    t =>
                    new
                    {
                        t.Id,
                        t.Name,
                        t.HomeMatches
                    }
                )
                .ToListAsync();

            var boza = await context.Teams
                .Join(
                    context.Matches,
                    te => te.Id,
                    ma => ma.HomeTeamId,
                    (te, ma) => new
                    {
                        TeamId = te.Id,
                        TeamName = te.Name,
                        MatchId = ma.Id,
                        MatchDate = ma.MatchDate
                    }
                )
                .ToListAsync();

            var tarator = await context.Teams
                .GroupJoin(
                    context.Matches,
                    t => t.Id,
                    m => m.HomeTeamId,
                    (te, ma) => new { te, ma }
                )
                .SelectMany(
                    x => x.ma.DefaultIfEmpty(),
                    (x, m) => new
                    {
                        TeamId = x.te.Id,
                        TeamName = x.te.Name,
                        MatchId = m != null ? m.Id : (int?)null,
                        MatchDate = m != null ? m.MatchDate : (DateTime?)null
                    }
                )
                .Where(x => x.MatchId != null)
                .ToListAsync();
        }

        public async Task SingleTeamName()
        {
            var t = context.Teams.Select(t => new { t.Name }).ToListAsync();
        }

        public async Task FilteringWithRelatedData()
        {
            var teams = await context.Leagues.Where(l => l.Teams.Any(t => t.Name.Contains("manchester"))).ToListAsync();
        }

        public async Task QueryViewAsync()
        {
            var teamsCoachesLeagues = await context.TeamsCoachesLeagues.ToListAsync();
            foreach (var item in teamsCoachesLeagues)
            {
                Console.WriteLine($"Team Name: {item.Name}, Coach Name: {item.CoachName}, League Name: {item.LeagueName}");
            }
        }

        public async Task RawSQLQueryAsync()
        {
            var name = "AS Roma";

            /// Do not use FromSqlRaw with string interpolation or concatenation, as it can lead to SQL injection vulnerabilities. Instead, use FromSqlInterpolated for parameterized queries.
            var teams1 = await context
                            .Teams
                            .FromSqlRaw($"Select * from Teams where name = '{name}'")
                            .Include(q => q.Coach)
                            .ToListAsync();

            var teams2 = await context
                            .Teams
                            .FromSqlInterpolated($"Select * from Teams where name = {name}")
                            .ToListAsync();

        }

        public async Task ExecuteNonQueryCommandAsync()
        {
            var teamId = 10;
            var affectedRows = await context.Database.ExecuteSqlRawAsync("exec sp_DeleteTeamById {0}", teamId);

            var teamId2 = 12;
            var affectedRows2 = await context.Database.ExecuteSqlInterpolatedAsync($"exec sp_DeleteTeamById {teamId2}");
        }

        public async Task ExecStoredProcedureAsync()
        {
            var teamId = 3;
            var result = await context.Coaches.FromSqlInterpolated($"EXEC dbo.sp_GetTeamCoach {teamId}").ToListAsync();
        }

        ///hmm , get the result in memory and then use the firstordefault method to get the first record in memory
        public async Task ExecStoredProceInMemoryAsync()
        {
            var teamId = 3;
            var result = (await context.Coaches
                            .FromSqlInterpolated($"EXEC dbo.sp_GetTeamCoach {teamId}")
                            .ToListAsync()
                            )
                            .FirstOrDefault();
        }
    }
}
