using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Web.Models
{
    public class vmShow
    {
        public vmShow(IEnumerable<ISeries> featuredSeries, IEnumerable<IVideo> featuredMovies)
        {
            FeaturedMovies = featuredMovies;
            FeaturedSeries = featuredSeries;
        }

        public IEnumerable<ISeries> FeaturedSeries { get; set; }

        public IEnumerable<IVideo> FeaturedMovies { get; set; }
    }
}