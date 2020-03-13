using System.Collections.Generic;

namespace Telegenic.Entities.Interfaces
{
    public interface ISeason : IEntityBase
    {
        IEnumerable<IVideo> Episodes { get; set; }

        int Season_Number { get; set; }

        int EpisodeCount();
    }
}