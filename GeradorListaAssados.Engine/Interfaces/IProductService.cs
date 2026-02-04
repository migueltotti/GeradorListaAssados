using GeradorListaAssados.Engine.Models;
using GeradorListaAssados.Engine.Result;

namespace GeradorListaAssados.Engine.Interfaces;

public interface IProductService
{
    Task<Product?> GetOneAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Result<Product>> AddAsync(Product product, CancellationToken cancellationToken);
    Task<Result<Product>> UpdateAsync(Product product, CancellationToken cancellationToken);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<string>> GenerateProductsListExcelFileAsync(string path, CancellationToken cancellationToken);
}
