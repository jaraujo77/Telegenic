using System.Collections.Generic;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface IGenreRepository : IRepositoryBase
    {
        IEnumerable<IGenre> GetByTitle(string _name);
    }
}
