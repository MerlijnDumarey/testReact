using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.Services.Interfaces
{
    public interface IAttemptsService
    {
        Task CreateAsync(Attempt newAttempt, string lectorId, string studentId, string testId);
        Task<List<Attempt>> GetAsync();
        Task<Attempt?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Attempt updatedAttempt, string lectorId, string studentId, string testId);
    }
}
