using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.Services.Interfaces
{
    public interface ILectorsService
    {
        Task CreateAsync(Lector newLector);
        Task<List<Lector>> GetAsync();
        Task<Lector?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Lector updatedLector);
    }
}