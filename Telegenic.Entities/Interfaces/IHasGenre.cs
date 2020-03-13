using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Models;

namespace Telegenic.Entities.Interfaces
{
    public interface IHasGenre
    {
        Genre Genre { get; set; }
    }
}
