using EntityFrameworkCore.Domain.Common;
using System.Collections.ObjectModel;

namespace EntityFrameworkCore.Domain
{
    public class Team : BaseDomainObject
    {
        public required string Name { get; set; }

        public int LeagueId { get; set; } // Foreign key property
        public League? League { get; set; }

        public Coach? Coach { get; set; }

        public Collection<Match> HomeMatches { get; set; } = [];
        public Collection<Match> AwayMatches { get; set; } = [];
    }
}
