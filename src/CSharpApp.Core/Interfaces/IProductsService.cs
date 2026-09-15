using CSharpApp.Core.Dtos.Product;

namespace CSharpApp.Core.Interfaces;

public interface IProductsService
{
    public Task<IReadOnlyCollection<ProductDetails>> GetAll();
    public Task<ProductDetails?> GetById(long id);
    public Task<ProductCreationResult> Create(ProductCreation product);
}