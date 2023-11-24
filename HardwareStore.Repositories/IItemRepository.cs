using HardwareStore.Entities;

namespace HardwareStore.Repositories;

public interface IItemRepository : IRepositoryBase<Items>
{
    Task<ICollection<Items>> ListAsyncByCategory(int? category);

}