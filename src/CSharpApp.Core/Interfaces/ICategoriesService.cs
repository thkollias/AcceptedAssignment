using CSharpApp.Core.Dtos.Category;

namespace CSharpApp.Core.Interfaces;

public interface ICategoriesService
{
    public Task<IReadOnlyCollection<CategoryDetails>> GetAll();
    public Task<CategoryDetails?> GetById(int id);
    public Task<CategoryCreationResult> Create(CategoryCreation category);
}
