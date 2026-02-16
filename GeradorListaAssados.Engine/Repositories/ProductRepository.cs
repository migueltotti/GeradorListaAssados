using GeradorListaAssados.Engine.Context;
using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GeradorListaAssados.Engine.Repositories
{
    public class ProductRepository(GeneratorDbContext context) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<Product?> GetOneAsync(Expression<Func<Product, bool>> expression, CancellationToken cancellationToken)
        {
            return context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(expression, cancellationToken);
        }

        public void Insert(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            context.ChangeTracker.Clear();
        }

        public void Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
            context.ChangeTracker.Clear();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
            context.ChangeTracker.Clear();
        }
    }
}
