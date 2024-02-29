using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.Services.Interfaces
{
    public interface ITestsService
    {
        Task CreateAsync(string name, string categoryId, string description, string unit, ComparisonType comparisonType, double? lowerBound, double? higherBound);
        Task<List<Test>> GetAsync();
        Task<Test?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, string name, string categoryId, string description, string unit, ComparisonType comparisonType, double? lowerBound, double? higherBound);
    }
}