using System.Data;
using Microsoft.EntityFrameworkCore;
using HardwareStore.Entities;
using HardwareStore.Persistence;
using HardwareStore.Repositories;

namespace HardwareStore.Repositories;

public class InvoiceRepository : RepositoryBase<Invoices>, IInvoiceRepository
{
    public InvoiceRepository(HardwareStoreDbContext context) 
        : base(context)
    {
    }

    public async Task CreateTransactionAsync()
    {
        await Context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
    }

    public override async Task UpdateAsync()
    {
        await Context.Database.CommitTransactionAsync();
        await base.UpdateAsync();
    }

    public async Task RollBackAsync()
    {
      
        if (Context.Database.CurrentTransaction != null)
        {
            await Context.Database.CurrentTransaction.RollbackAsync();
        }
    }



    public override async Task<int> AddAsync(Invoices entity)
    {
        entity.SaleDate = DateTime.Now;
        var lastNumber = await Context.Set<Invoices>().CountAsync() + 1;
        entity.DocNum = $"{lastNumber:000000}";

        // Agregar la entidad de forma implícita
        await Context.AddAsync(entity);

        // Guardar cambios en la base de datos para obtener el ID generado
        await Context.SaveChangesAsync();

        // Devolver el ID generado después de la inserción
        return entity.Id;
    }

    public override async Task<Invoices?> FindByIdAsync(int id)
    {
        return await Context.Set<Invoices>()
            .Include(p => p.Customer)
            .Where(p => p.Id == id)
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();
    }
}