using System.Collections.Generic;

namespace Telegenic.Entities.Interfaces
{
    public interface ISeason : IEntityBase
    {
        IEnumerable<IEpisode> Episodes { get; set; }

        int Season_Number { get; set; }

        int EpisodeCount();

        int Series_Id { get; set; }
    }
}