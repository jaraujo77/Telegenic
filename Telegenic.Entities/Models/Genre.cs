using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Entities.Models
{
    public class Genre : IGenre
    {
        public virtual int Id { get; set; }

        public virtual string Title
        {
            get
            {
                return this.Name;

            }
            set
            {
                this.Name = value;

            }
        }

        [Display(Name="Genre Name")]
        public virtual string Name { get; set; }
    }
}
