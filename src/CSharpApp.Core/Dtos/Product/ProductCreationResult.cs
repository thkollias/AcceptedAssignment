using CSharpApp.Core.Dtos.Category;

namespace CSharpApp.Core.Dtos.Product;

public class ProductCreationResult
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("price")]
    public int? Price { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = [];
    
    [JsonPropertyName("category")]
    public CategoryDetails? Category { get; set; }
    
    [JsonPropertyName("creationAt")]
    public DateTime? CreationAt { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime? UpdatedAt { get; set; }
}
