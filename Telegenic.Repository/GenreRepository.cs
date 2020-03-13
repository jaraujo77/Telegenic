using NHibernate;
using System.Collections.Generic;
using System.Linq;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ISession session) : base(session) { }

        public IEnumerable<IGenre> GetByTitle(string _name)
        {
            var query = _session.Query<Genre>().Where(x => x.Name.StartsWith(_name));
            return query.ToList<Genre>();
        }
    }
}
