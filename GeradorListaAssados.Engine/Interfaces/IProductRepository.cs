using GeradorListaAssados.Engine.Models;

namespace GeradorListaAssados.Engine.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetOneAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken);
    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
}
