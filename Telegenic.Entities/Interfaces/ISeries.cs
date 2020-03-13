using System.Collections.Generic;


namespace Telegenic.Entities.Interfaces
{
    public interface ISeries : IEntityBase
    {        
        IEnumerable<ISeason> Seasons { get; set; }

        int SeasonCount();
    }
}