using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Types;

namespace Telegenic.Entities.Interfaces
{
    public interface IHasRating
    {
        RatingType Rating { get; set; }
    }
}
