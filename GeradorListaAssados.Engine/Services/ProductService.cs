using ClosedXML.Excel;
using FluentValidation;
using GeradorListaAssados.Engine.Extentions;
using GeradorListaAssados.Engine.Interfaces;
using GeradorListaAssados.Engine.Models;
using GeradorListaAssados.Engine.Result;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GeradorListaAssados.Engine.Services
{
    public class ProductService(
        IProductRepository productRepository,
        IValidator<Product> validator,
        ILogger<ProductService> logger) : IProductService
    {
        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync(cancellationToken);

            return products;
        }

        public async Task<Product?> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetOneAsync(x => x.Id == id, cancellationToken);

            return product;
        }

        public async Task<Product?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetOneAsync(x => x.Name.Contains(name), cancellationToken);

            return product;
        }

        public async Task<Result<Product>> AddAsync(Product product, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                logger.LogWarning("Product validation failed: {Errors}", result.Errors);
                return Result<Product>.Failure(ProductErrors.ValidationError(JsonSerializer.Serialize(result.Errors)));
            }

            var productExist = await GetOneAsync(product.Id, cancellationToken);
            if (productExist is not null)
            {
                logger.LogWarning("Product with Id: {Id} already exists.", product.Id);
                return Result<Product>.Failure(ProductErrors.AlreadyExists);
            }

            var newProduct = Product.Builder.Create()
                .SetName(product.Name)
                .SetPrice(product.Price)
                .SetQuantity(product.Quantity)
                .SetIndex(product.Index)
                .SetColor(product.HexCodeColor)
                .Build();

            productRepository.Insert(newProduct);

            return Result<Product>.Success(product);
        }

        public async Task<Result<Product>> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(product);
            if (!result.IsValid)
            {
                logger.LogWarning("Product validation failed: {Errors}", result.Errors);
                return Result<Product>.Failure(ProductErrors.ValidationError(JsonSerializer.Serialize(result.Errors)));
            }

            var productExist = await GetOneAsync(product.Id, cancellationToken);
            if (productExist is null)
            {
                logger.LogWarning("Product with Id: {Id} not found.", product.Id);
                return Result<Product>.Failure(ProductErrors.NotFound);
            }

            var updatedProduct = productExist.ToBuilder()
                .SetName(product.Name)
                .SetPrice(product.Price)
                .SetQuantity(product.Quantity)
                .SetIndex(product.Index)
                .SetColor(product.HexCodeColor)
                .Build();

            productRepository.Update(updatedProduct);

            return Result<Product>.Success(product);
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await GetOneAsync(id, cancellationToken);
            if (product is null)
            {
                logger.LogWarning("Product with Id: {Id} not found.", id);
                return Result<bool>.Failure(ProductErrors.NotFound);
            }

            productRepository.Delete(product);

            return Result<bool>.Success(true);
        }

        public async Task<Result<string>> GenerateProductsListExcelFileAsync(string path, CancellationToken cancellationToken)
        {
            if (!Path.Exists(path))
            {
                logger.LogWarning("Provided Path is invalid - Path {Path}", path);
                return Result<string>.Failure(PathErrors.InvalidPath);
            }

            var filePath = Path.Combine(path, $"lista-{DateTime.Now:dd-MM-yyyy}.xlsx");

            var products = await GetAllAsync(cancellationToken);
            if (!products.Any())
            {
                logger.LogWarning("No products found to generate the Excel file.");
                return Result<string>.Failure(ProductErrors.NoProductsFound);
            }

            products
                .ToList()
                .Sort((p1, p2) => p1.Index < p2.Index ? -1 : 1);

            var workbook = new XLWorkbook();

            workbook
                .CreateWorksheet()
                .ConfigureRowsAndColumn()
                .AddHeader()
                .AddProducts(products);

            workbook.SaveAs(filePath);

            return Result<string>.Success(filePath);
        }
    }
}
