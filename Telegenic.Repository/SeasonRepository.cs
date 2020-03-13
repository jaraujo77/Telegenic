﻿using NHibernate;
using System.Collections.Generic;
using System.Linq;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;
using Telegenic.Repository.Interfaces;

namespace Telegenic.Repository
{
    public class SeasonRepository : BaseRepository<Season>, ISeasonRepository
    {
        public SeasonRepository(ISession session) : base(session) { }

        public IEnumerable<ISeason> GetByTitle(string _title)
        {
            var query = _session.Query<Season>().Where(x => x.Title.StartsWith(_title));
            return query.ToList<Season>();
        }
    }
}
