using System.Collections.Generic;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface ISeasonRepository : IRepositoryBase
    {
        IEnumerable<ISeason> GetByTitle(string _title);

        IEnumerable<ISeason> GetSeasonsBySeriesId(int _seriesId);
    }
}
