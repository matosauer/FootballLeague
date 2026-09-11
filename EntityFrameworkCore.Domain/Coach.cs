using EntityFrameworkCore.Domain.Common;

namespace EntityFrameworkCore.Domain
{
    public class Coach : BaseDomainObject
    {
        public required string Name { get; set; }
        public int? TeamId { get; set; }
        public virtual Team? Team { get; set; }
    }
}
