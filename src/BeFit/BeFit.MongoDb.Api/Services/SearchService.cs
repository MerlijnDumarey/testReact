using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeFit.MongoDb.Api.Services
{
    public class SearchService : ISearchService
    {
        private readonly IMongoCollection<Attempt> _attemptsCollection;
        private readonly IMongoCollection<Test> _testCollection;
        private readonly ICategoriesService _categoriesService;
        private readonly ITestsService _testsService;
        private readonly IStudentService _studentService;
        private readonly ILectorsService _lectorsService;
        public SearchService(IOptions<BeFitDatabaseSettings> beFitDatabaseSettings)
        {
            var mongoClient = new MongoClient(beFitDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(beFitDatabaseSettings.Value.DatabaseName);
            _attemptsCollection = mongoDatabase.GetCollection<Attempt>(beFitDatabaseSettings.Value.AttemptsCollectionName);
            _testCollection = mongoDatabase.GetCollection<Test>(beFitDatabaseSettings.Value.TestsCollectionName);

            _categoriesService = new CategoriesService(beFitDatabaseSettings);
            _testsService = new TestsService(beFitDatabaseSettings, _categoriesService);
            _lectorsService = new LectorsService(beFitDatabaseSettings);
            _studentService = new StudentService(beFitDatabaseSettings);
        }
        public Task<List<Attempt>> GetAttemptsByStudent(string studentId)
        {
            var attempts = _attemptsCollection.Find(a => a.Student.Id.Equals(studentId)).ToListAsync();
            return attempts;
        }
        public Task<List<Attempt>> GetAttemptsByLector(string lectorId)
        {
            var attempts = _attemptsCollection.Find(a => a.Lector.Id.Equals(lectorId)).ToListAsync();
            return attempts;
        }

        public Task<List<Attempt>> GetAttemptsByTest(string testId)
        {
            var attempts = _attemptsCollection.Find(a => a.Test.Id.Equals(testId)).ToListAsync();
            return attempts;
        }

        public Task<List<Test>> GetTestsByCategory(string categoryId)
        {
            var tests = _testCollection.Find(t => t.Category.Id.Equals(categoryId)).ToListAsync();
            return tests;
        }
    }
}
