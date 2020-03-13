using NHibernate;
using System.Collections.Generic;
using System.Linq;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Repository
{
    public class EpisodeRepository : BaseRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(ISession session) : base(session) { }

        public IEnumerable<IEpisode> GetByTitle(string _title)
        {
            var query = _session.Query<Episode>().Where(x => x.Title.StartsWith(_title));
            return query.ToList<Episode>();
        }
    }
}
