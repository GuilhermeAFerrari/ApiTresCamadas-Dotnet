using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<Supplier> GetSupplierAddressAsync(Guid id);
    Task<Supplier> GetSupplierProductsAddressAsync(Guid id);
    Task<Address> GetAddressBySupplierAsync(Guid supplierId);
    Task RemoveAddressSupplier(Address address);
}
