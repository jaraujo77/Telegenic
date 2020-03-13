using NHibernate;
using System.Collections.Generic;
using System.Linq;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(ISession session) : base(session) { }

        public IEnumerable<IVideo> GetByTitle(string _title)
        {
            var query = _session.Query<Movie>().Where(x => x.Title.StartsWith(_title));
            return query.ToList<Movie>();
        }

        public IEnumerable<IVideo> GetFeaturedItems()
        {
            var query = _session.Query<Movie>();//TODO add filter             
            return query.ToList<Movie>();
        }
    }
}
