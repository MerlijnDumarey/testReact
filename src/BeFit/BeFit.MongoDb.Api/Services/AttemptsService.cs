using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeFit.MongoDb.Api.Services
{
    public class AttemptsService : IAttemptsService
    {
        private readonly IMongoCollection<Attempt> _attemptsCollection;
        private readonly ICategoriesService _categoriesService;
        private readonly ITestsService _testsService;
        private readonly IStudentService _studentService;
        private readonly ILectorsService _lectorsService;

        public AttemptsService(IOptions<BeFitDatabaseSettings> beFitDatabaseSettings)
        {
            var mongoClient = new MongoClient(beFitDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(beFitDatabaseSettings.Value.DatabaseName);
            _attemptsCollection = mongoDatabase.GetCollection<Attempt>(beFitDatabaseSettings.Value.AttemptsCollectionName);
            
            _categoriesService = new CategoriesService(beFitDatabaseSettings);
            _testsService = new TestsService(beFitDatabaseSettings, _categoriesService);
            _lectorsService = new LectorsService(beFitDatabaseSettings);
            _studentService = new StudentService(beFitDatabaseSettings);
        }
        public async Task CreateAsync(Attempt newAttempt, string lectorId, string studentId, string testId)
        {
            var lector = await _lectorsService.GetAsync(lectorId);
            newAttempt.Lector = new AttemptLectorModel()
            { 
                Id = lector.Id,
                Name = lector.Name,
                FamilyName = lector.FamilyName,
            };
            var student = await _studentService.GetAsync(studentId);
            newAttempt.Student = new AttemptStudentModel()
            {
                Id = student.Id,
                Name = student.Name,
                FamilyName = student.FamilyName,
                CurrentSemester = student.CurrentSemester,
                Gender = student.Gender,
            };
            newAttempt.Test = await _testsService.GetAsync(testId);
            await _attemptsCollection.InsertOneAsync(newAttempt);
        }

        public async Task<List<Attempt>> GetAsync()
        {
            return await _attemptsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Attempt?> GetAsync(string id)
        {
            return await _attemptsCollection.Find(a => a.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(string id)
        {
            await _attemptsCollection.DeleteOneAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(string id, Attempt updatedAttempt, string lectorId, string studentId, string testId)
        {
            var lector = await _lectorsService.GetAsync(lectorId);
            updatedAttempt.Lector = new AttemptLectorModel()
            {
                Id = lector.Id,
                Name = lector.Name,
                FamilyName = lector.FamilyName,
            };
            var student = await _studentService.GetAsync(studentId);
            updatedAttempt.Student = new AttemptStudentModel()
            {
                Id = student.Id,
                Name = student.Name,
                FamilyName = student.FamilyName,
                CurrentSemester = student.CurrentSemester,
                Gender = student.Gender,
            };
            updatedAttempt.Test = await _testsService.GetAsync(testId);
            await _attemptsCollection.ReplaceOneAsync(a => a.Id == id, updatedAttempt);
            
        }
    }
}
