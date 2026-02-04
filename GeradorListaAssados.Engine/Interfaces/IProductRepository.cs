using GeradorListaAssados.Engine.Models;
using System.Linq.Expressions;

namespace GeradorListaAssados.Engine.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetOneAsync(Expression<Func<Product, bool>> expression, CancellationToken cancellationToken);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken);
    void Insert(Product product);
    void Update(Product product);
    void Delete(Product product);
}
