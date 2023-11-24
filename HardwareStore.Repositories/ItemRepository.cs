using HardwareStore.Entities;
using HardwareStore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HardwareStore.Repositories
{
    public class ItemRepository : RepositoryBase<Items>, IItemRepository
    {
        public ItemRepository(HardwareStoreDbContext context) 
            : base(context)
        {
        }

        public async Task<ICollection<Items>> ListAsyncByCategory(int? category)
        {
            IQueryable<Items> query = Context.Set<Items>();

            if (category.HasValue)
            {
                query = query.Where(p => p.CategoryId.Equals(category.Value));
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}