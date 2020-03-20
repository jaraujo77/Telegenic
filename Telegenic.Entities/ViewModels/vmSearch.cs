using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;

namespace Telegenic.Entities.ViewModels
{
    public class vmSearch
    {
        public vmSearch() { }

        public vmSearch(string searchHeading)
        {
            this.SearchHeading = searchHeading;
        }

        public string SearchTerm { get; set; }

        public string SearchHeading { get; set; }

        
    }
}
