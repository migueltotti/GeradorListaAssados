using GeradorListaAssados.Engine.Context;
using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using Microsoft.EntityFrameworkCore;

namespace GeradorListaAssados.Engine.Repositories
{
    internal class ProductRepository(GeneratorDbContext context) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<Product?> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            return context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Name.Contains(name), cancellationToken);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
