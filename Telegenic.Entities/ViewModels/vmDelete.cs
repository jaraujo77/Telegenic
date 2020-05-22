using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegenic.Entities.Interfaces;

namespace Telegenic.Entities.ViewModels
{
    public class vmDelete<T> where T : IEntityBase
    {
        public vmDelete() { }

        public vmDelete(T entity)
        {
            Entity = entity;
        }

        public string PageHeading { get; set; }

        public string ValidationMessage { get; set; }

        public string ErrorMessage { get; set; }

        public T Entity { get; set; }

        public bool EntityHasRelatedItems { get; set; }
    }
}
