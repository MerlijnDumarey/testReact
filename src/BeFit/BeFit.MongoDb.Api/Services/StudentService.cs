using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeFit.MongoDb.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<Attempt> _attemptsCollection;
        public StudentService(IOptions<BeFitDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(settings.Value.DatabaseName);

            _students = mongoDatabase.GetCollection<Student>(settings.Value.StudentsCollectionName);
            _attemptsCollection = mongoDatabase.GetCollection<Attempt>(settings.Value.AttemptsCollectionName);
        }
        public async Task CreateAsync(Student newStudent)
        {
            await _students.InsertOneAsync(newStudent);
        }
        public async Task UpdateAsync(string id, Student updatedStudent)
        {
            await _students.ReplaceOneAsync(student => student.Id == id, updatedStudent);
            var updatedStudentModel = new AttemptStudentModel()
            {
                Id = updatedStudent.Id,
                Name = updatedStudent.Name,
                FamilyName = updatedStudent.FamilyName,
                Gender = updatedStudent.Gender,
                CurrentSemester = updatedStudent.CurrentSemester,
            };
            var filter = Builders<Attempt>.Filter.Eq(a => a.Student.Id, id);
            var update = Builders<Attempt>.Update.Set(a => a.Student, updatedStudentModel);
            await _attemptsCollection.UpdateManyAsync(filter, update);
        }
        public async Task RemoveAsync(string id)
        {
            await _students.DeleteOneAsync(student => student.Id == id);
        }


        public async Task<List<Student>> GetAsync()
        {
            return await _students.Find(_ => true).ToListAsync();
        }

        public async Task<Student?> GetAsync(string id)
        {
            return await _students.Find(student => student.Id == id).FirstOrDefaultAsync();
        }
    }
}
