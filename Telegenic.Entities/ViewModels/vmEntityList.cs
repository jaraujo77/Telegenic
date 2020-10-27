using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Entities.ViewModels
{
    public class vmEntityList<T> where T : IEntityBase
    {
        public vmEntityList(IEnumerable<T> entities)
        {
            Entities = entities;
        }

        public vmEntityList(IEnumerable<T> entities, string pageHeading)
        {
            Entities = entities;
            PageHeading = pageHeading;
        }

        public IEnumerable<T> Entities { get; set; }

        public string PageHeading { get; set; }

        public IEntityBase SearchEntity { get; set; }
    }
}
