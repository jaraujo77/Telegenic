using FluentNHibernate.Mapping;
using Telegenic.Entities.Models;
using Telegenic.Entities.Types;

namespace Telegenic.Web.Plumbing.nHibernate.Maps
{
    public static class BaseMaps
    {
        public class MovieMap : ClassMap<Movie>
        {
            public MovieMap()
            {
                Table("movie");
                Id(x => x.Id);
                Map(x => x.ElenaFriendly)
                    .Column("elena_friendly");
                Map(x => x.PlaceholderImagePath)
                    .Column("placeholder_image_path");
                Map(x => x.Runtime)
                    .CustomType("TimeAsTimeSpan");
                Map(x => x.Title);
                References<Genre>(x => x.Genre);
                Map(x => x.Rating, "rating_id").CustomType<RatingType>();
            }
        }

        public class EpisodeMap : ClassMap<Episode>
        {
            public EpisodeMap()
            {
                Table("episode");
                Id(x => x.Id);
                Map(x => x.ElenaFriendly)
                    .Column("elena_friendly");
                Map(x => x.EpisodeNumber)
                    .Column("episode_number");
                Map(x => x.PlaceholderImagePath)
                    .Column("placeholder_image_path");
                Map(x => x.Runtime)
                    .CustomType("TimeAsTimeSpan");

                Map(x => x.Title);
                Map(x => x.Rating, "rating_id").CustomType<RatingType>();
            }
        }

        public class SeasonMap : ClassMap<Season>
        {
            public SeasonMap()
            {
                Table("season");
                Id(x => x.Id);
                Map(x => x.Season_Number);
                Map(x => x.Title);
                HasMany<Episode>(x => x.Episodes).KeyColumn("season_id").Inverse().Cascade.All();
            }
        }

        public class SeriesMap : ClassMap<Series>
        {
            public SeriesMap()
            {
                Table("series");
                Id(x => x.Id);
                HasMany<Season>(x => x.Seasons).KeyColumn("series_id").Inverse().Cascade.All();
                Map(x => x.Title);
                References<Genre>(x => x.Genre);
            }
        }

        public class GenreMap : ClassMap<Genre>
        {
            public GenreMap()
            {
                Table("genre");
                Id(x => x.Id);
                Map(x => x.Name);
            }
        }

    }
}