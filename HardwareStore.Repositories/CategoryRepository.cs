using HardwareStore.Entities;
using HardwareStore.Persistence;

namespace HardwareStore.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(HardwareStoreDbContext context) 
            : base(context)
        {
        }
    }
}