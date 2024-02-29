using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.Services.Interfaces
{
    public interface ISearchService
    {
        Task<List<Attempt>> GetAttemptsByStudent(string studentId);
        Task<List<Attempt>> GetAttemptsByLector(string lectorId);
        Task<List<Attempt>> GetAttemptsByTest(string testId);
        Task<List<Test>> GetTestsByCategory(string categoryId);
    }
}
