using NHibernate;
using System.Collections.Generic;
using System.Linq;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Repository
{
    public class SeriesRepository : BaseRepository<Series>, ISeriesRepository
    {
        public SeriesRepository(ISession session) : base(session) { }

        public IEnumerable<ISeries> GetByTitle(string _title)
        {
            var query = _session.Query<Series>().Where(x => x.Title.StartsWith(_title));
            return query.OrderBy(x => x.Title).ToList<Series>();
        }

        public IEnumerable<ISeries> GetFeaturedItems()
        {
            var query = _session.Query<Series>();//TODO add filter
            return query.ToList<Series>();
        }
    }
}
