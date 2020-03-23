using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Entities.Models
{
    public class Series : ISeries, IHasGenre
    {
        public virtual int Id { get; set; }

        [Display(Name= "Series Title")]
        public virtual string Title { get; set; }

        [Display(Name = "Seasons")]
        public virtual IEnumerable<ISeason> Seasons { get; set; }

        public virtual Genre Genre { get; set; }

        [Display(Name = "Season Count")]
        public virtual int SeasonCount
        {
            get
            {
                return Seasons != null ? Seasons.Count() : 0;
            }
        }
    }
}
