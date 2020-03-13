using System.Collections.Generic;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface IMovieRepository : IRepositoryBase
    {
        IEnumerable<IVideo> GetByTitle(string _title);

        IEnumerable<IVideo> GetFeaturedItems();
    }
}
