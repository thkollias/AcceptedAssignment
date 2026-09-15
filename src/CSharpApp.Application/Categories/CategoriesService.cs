using CSharpApp.Core.Dtos.Category;
using System.Net.Http.Json;

namespace CSharpApp.Application.Categories;

public class CategoriesService : ICategoriesService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CategoriesService> _logger;

    public CategoriesService(
        HttpClient httpClient,
        ILogger<CategoriesService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<CategoryDetails>> GetAll()
    {
        var categories = await _httpClient
            .GetFromJsonAsync<List<CategoryDetails>>("categories");

        return categories ?? [];
    }

    public async Task<CategoryDetails?> GetById(int id)
    {
        var product = await _httpClient
            .GetFromJsonAsync<CategoryDetails>($"categories/{id}");

        return product;
    }

    public async Task<CategoryCreationResult> Create(CategoryCreation category)
    {
        var response = await _httpClient.PostAsJsonAsync("categories", category);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Downstream API returned {response.StatusCode}: {error}");
        }

        return await response.Content.ReadFromJsonAsync<CategoryCreationResult>();
    }
}
