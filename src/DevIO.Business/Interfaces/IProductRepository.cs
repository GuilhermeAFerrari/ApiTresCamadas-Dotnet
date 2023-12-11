using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetBySupplierAsync(Guid supplierId);
    Task<IEnumerable<Product>> GetProductsSuppliersAsync();
    Task<Product> GetProductSupplierAsync(Guid id);
}
