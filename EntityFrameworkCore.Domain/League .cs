using EntityFrameworkCore.Domain.Common;

namespace EntityFrameworkCore.Domain
{
    public class League : BaseDomainObject
    {
        public required string Name { get; set; }
        public ICollection<Team> Teams { get; set; } = [];
    }
}
