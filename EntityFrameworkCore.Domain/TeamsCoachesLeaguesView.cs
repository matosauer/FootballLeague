namespace EntityFrameworkCore.Domain
{
    public class TeamsCoachesLeaguesView
    {
        public required string Name { get; set; }
        public string? CoachName { get; set; }
        public string? LeagueName { get; set; }
    }
}
