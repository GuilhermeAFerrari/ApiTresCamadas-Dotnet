using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services;

public class SupplierService : BaseService, ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task AddAsync(Supplier supplier)
    {
        if (!ExecuteValidation(new SupplierValidation(), supplier) ||
            !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

        if (_supplierRepository.SearchAsync(s => s.Document == supplier.Document).Result.Any())
        {
            Notifier("There is already a supplier with the informed document");
            return;
        }

        await _supplierRepository.AddAsync(supplier);
    }

    public async Task UpdateAsync(Supplier supplier)
    {
        if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

        if (_supplierRepository.SearchAsync(
                s => s.Document == supplier.Document &&
                s.Id != supplier.Id
            ).Result.Any()
        )
        {
            Notifier("There is already a supplier with the informed document");
            return;
        }
        await _supplierRepository.UpdateAsync(supplier);
    }

    public async Task RemoveAsync(Guid id)
    {
        var supplier = await _supplierRepository.GetSupplierProductsAddressAsync(id);

        if (supplier is null)
        {
            Notifier("Supplier does not exist");
            return;
        }

        if (supplier.Products.Any())
        {
            Notifier("Supplier has registered products");
            return;
        }

        var address = await _supplierRepository.GetAddressBySupplierAsync(id);

        if (address is not null)
        {
            await _supplierRepository.RemoveAddressSupplier(address);
        }

        await _supplierRepository.RemoveAsync(id);
    }

    public void Dispose()
    {
        _supplierRepository?.Dispose();
    }
}
