namespace CSharpApp.Core.Dtos.Category;

public class CategoryCreation
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
