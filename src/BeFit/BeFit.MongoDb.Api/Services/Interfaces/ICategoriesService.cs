using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task CreateAsync(Category newCategory);
        Task<List<Category>> GetAsync();
        Task<Category?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Category updatedCategory);
    }
}