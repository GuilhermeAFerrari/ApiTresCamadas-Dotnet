using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(
        IProductRepository productRepository,
        INotifier notifier
    ) : base(notifier)
    {
        _productRepository = productRepository;
    }

    public async Task AddAsync(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;

        var existingProduct = await _productRepository.GetByIdAsync(product.Id);

        if (existingProduct is not null)
        {
            Notify("There is already a product with the specified ID");
            return;
        }

        await _productRepository.AddAsync(product);
    }

    public async Task UpdateAsync(Product product)
    {
        if (!ExecuteValidation(new ProductValidation(), product)) return;
        await _productRepository.UpdateAsync(product);
    }

    public async Task RemoveAsync(Guid id)
    {
        await _productRepository.RemoveAsync(id);
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
    }
}
