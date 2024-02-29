using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateAsync(Student newStudent);
        Task<List<Student>> GetAsync();
        Task<Student?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Student updatedStudent);
    }
}
