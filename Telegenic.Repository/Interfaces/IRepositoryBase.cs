using Telegenic.Entities.Interfaces;

namespace Telegenic.Repository.Interfaces
{
    public interface IRepositoryBase
    {
        void Save(IEntityBase entity);

        bool Delete(IEntityBase entity);
    }
}
