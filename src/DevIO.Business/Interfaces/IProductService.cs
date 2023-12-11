using DevIO.Business.Models;

namespace DevIO.Business.Interfaces;

public interface IProductService : IDisposable
{
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task RemoveAsync(Guid id);
}
