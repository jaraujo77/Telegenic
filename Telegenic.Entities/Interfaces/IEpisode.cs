using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegenic.Entities.Interfaces
{
    public interface IEpisode : IVideo
    {
        int EpisodeNumber { get; set; }

        int Season_Id { get; set; }
    }
}
