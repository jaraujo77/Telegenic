
using Telegenic.Entities.Interfaces;


namespace Telegenic.Entities.Interfaces
{
    public interface IGenre : IEntityBase
    {
        string Name { get; set; }
    }
}
