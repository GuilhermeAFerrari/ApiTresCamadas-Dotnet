using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DatabaseContext dbContext) : base(dbContext) { }

    public async Task<IEnumerable<Product>> GetBySupplierAsync(Guid supplierId)
    {
        return await SearchAsync(p => p.SupplierId == supplierId);
    }

    public async Task<IEnumerable<Product>> GetProductsSuppliersAsync()
    {
        return await DbContext.Products.AsNoTracking()
            .Include(s => s.Supplier)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Product> GetProductSupplierAsync(Guid id)
    {
        return await DbContext.Products.AsNoTracking()
            .Include(s => s.Supplier)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}
