namespace CSharpApp.Core.Dtos.Category;

public class CategoryCreationResult
{
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
