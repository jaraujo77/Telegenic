using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface IRepositoryBase
    {
        void Save(IEntityBase entity);

        void Delete(IEntityBase entity);
    }
}
