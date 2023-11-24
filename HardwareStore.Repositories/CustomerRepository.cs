using Microsoft.EntityFrameworkCore;
using HardwareStore.Entities;
using HardwareStore.Persistence;

namespace HardwareStore.Repositories;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(HardwareStoreDbContext context) 
        : base(context)
    {
    }

    public async Task<Customer?> FindByEmailAsync(string email)
    {
        return await Context.Set<Customer>().FirstOrDefaultAsync(p => p.Email == email);
    }
}