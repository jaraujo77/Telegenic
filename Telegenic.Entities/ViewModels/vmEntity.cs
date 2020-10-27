using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;

namespace Telegenic.Entities.ViewModels
{
    public class vmEntity
    {
        public vmEntity() { }

        public vmEntity(IEnumerable<IGenre> genres)
        {
            Genres = genres;
        }

        public string PageHeading { get; set; }

        public Genre Genre { get; set; }

        public Movie Movie { get; set; }

        public Series Series { get; set; }

        public Season Season { get; set; }

        public Episode Episode { get; set; }

        public IEnumerable<IGenre> Genres { get; set; }

        private IEnumerable<SelectListItem> DefaultSelectListItem(string TextValue)
        {
            return Enumerable.Repeat(new SelectListItem
            {
                Value = "-1",
                Text = TextValue
            }, count: 1);
        }

        public IEnumerable<SelectListItem> SelectListItems(IEnumerable<IEntityBase> entities, string typeName)
        {
            var allEntities = entities.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            });

            return DefaultSelectListItem($"Select a {typeName}").Concat(allEntities.OrderBy(x => x.Text));
        }
    }
}
