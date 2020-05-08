using System.Collections.Generic;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface IEpisodeRepository : IRepositoryBase
    {
        IEnumerable<IEpisode> GetByTitle(string _title);

        IEnumerable<IEpisode> GetEpisodesBySeasonId(int _seasonId);
    }
}
