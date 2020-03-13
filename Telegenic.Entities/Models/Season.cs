using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Entities.Models
{
    public class Season : ISeason
    {
        public virtual int Id { get; set; }

        [Display(Name = "Season Title")]
        public virtual string Title { get; set; }

        [Display(Name = "Season Number")]
        public virtual int Season_Number { get; set; }

        public virtual IEnumerable<IVideo> Episodes { get; set; }

        public virtual int EpisodeCount()
        {
            return Episodes.Count();
        }

    }
}
