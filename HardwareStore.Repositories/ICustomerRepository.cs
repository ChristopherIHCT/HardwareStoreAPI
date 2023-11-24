using HardwareStore.Entities;

namespace HardwareStore.Repositories;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<Customer?> FindByEmailAsync(string email);
}