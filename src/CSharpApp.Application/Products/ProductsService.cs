using System.Net.Http.Json;

namespace CSharpApp.Application.Products;

public class ProductsService : IProductsService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(
        HttpClient httpClient,
        ILogger<ProductsService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(
        CancellationToken cancellationToken = default)
    {
        var products = await _httpClient
            .GetFromJsonAsync<List<Product>>("products", cancellationToken);

        return products ?? [];
    }
}