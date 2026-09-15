using CSharpApp.Core.Dtos.Product;
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

    public async Task<IReadOnlyCollection<ProductDetails>> GetAll()
    {
        var products = await _httpClient
            .GetFromJsonAsync<List<ProductDetails>>("products");

        return products ?? [];
    }

    public async Task<ProductDetails?> GetById(long id)
    {
        var product = await _httpClient
            .GetFromJsonAsync<ProductDetails>($"products/{id}");

        return product;
    }

    public async Task<ProductCreationResult> Create(ProductCreation product)
    {
        var response = await _httpClient.PostAsJsonAsync("products", product);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Downstream API returned {response.StatusCode}: {error}");
        }

        return await response.Content.ReadFromJsonAsync<ProductCreationResult>();
    }
}