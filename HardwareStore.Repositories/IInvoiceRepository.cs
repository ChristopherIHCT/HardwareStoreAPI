using HardwareStore.Entities;

namespace HardwareStore.Repositories;

public interface IInvoiceRepository : IRepositoryBase<Invoices>
{
    Task CreateTransactionAsync();

    Task RollBackAsync();
}