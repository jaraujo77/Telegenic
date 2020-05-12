using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Types;

namespace Telegenic.Entities.Models
{
    public class Episode : IEpisode, IHasRating
    {
        public virtual int Id { get; set; }

        public virtual string Title { get; set; }

        public virtual RatingType Rating { get; set; }

        [Display(Name = "Run Time")]
        public virtual TimeSpan Runtime { get; set; }

        [Display(Name = "Placeholder Image Path")]
        public virtual string PlaceholderImagePath { get; set; }

        [Display(Name = "Elena Friendly")]
        public virtual bool ElenaFriendly { get; set; }

        [Display(Name = "Episode Number")]
        public virtual int EpisodeNumber { get; set; }
        
        public virtual int Season_Id { get; set; }
    }
}
