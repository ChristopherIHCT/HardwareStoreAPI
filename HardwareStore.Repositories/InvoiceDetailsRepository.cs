using HardwareStore.Entities;
using HardwareStore.Persistence;

namespace HardwareStore.Repositories
{
    public class InvoiceDetailsRepository : RepositoryBase<InvoiceDetails>, IInvoiceDetailsRepository
    {
        public InvoiceDetailsRepository(HardwareStoreDbContext context) 
            : base(context)
        {
        }
    }
}