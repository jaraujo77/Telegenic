using System.Collections.Generic;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface ISeriesRepository : IRepositoryBase
    {
        IEnumerable<ISeries> GetByTitle(string _title);

        IEnumerable<ISeries> GetFeaturedItems();
    }
}
