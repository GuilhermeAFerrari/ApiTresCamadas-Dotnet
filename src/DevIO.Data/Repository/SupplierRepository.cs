using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository;

public class SupplierRepository : Repository<Supplier>, ISupplierRepository
{
    public SupplierRepository(DatabaseContext dbContext) : base(dbContext) { }

    public async Task<Supplier> GetSupplierAddressAsync(Guid id)
    {
        return await DbContext.Suppliers.AsNoTracking()
            .Include(a => a.Address)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Supplier> GetSupplierProductsAddressAsync(Guid id)
    {
        return await DbContext.Suppliers.AsNoTracking()
            .Include(p => p.Products)
            .Include(a => a.Address)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Address> GetAddressBySupplierAsync(Guid supplierId)
    {
        return await DbContext.Adresses.AsNoTracking()
            .FirstOrDefaultAsync(s => s.SupplierId == supplierId);
    }

    public async Task RemoveAddressSupplier(Address address)
    {
        DbContext.Adresses.Remove(address);
        await SaveChanges();
    }
}
