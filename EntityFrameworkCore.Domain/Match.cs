using EntityFrameworkCore.Domain.Common;

namespace EntityFrameworkCore.Domain
{
    public class Match : BaseDomainObject
    {
        public int HomeTeamId { get; set; }
        public Team? HomeTeam { get; set; }

        public int AwayTeamId { get; set; }
        public Team? AwayTeam { get; set; }

        public DateTime MatchDate { get; set; } = DateTime.Now;
    }
}
