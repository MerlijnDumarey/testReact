using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Xml.Linq;

namespace BeFit.MongoDb.Api.Services
{
    public class TestsService : ITestsService
    {
        private readonly IMongoCollection<Test> _testsCollection;
        private readonly IMongoCollection<Attempt> _attemptsCollection;
        private readonly ICategoriesService _categoriesService;

        public TestsService(IOptions<BeFitDatabaseSettings> beFitDatabaseSettings, ICategoriesService categoriesService)
        {
            var mongoClient = new MongoClient(beFitDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(beFitDatabaseSettings.Value.DatabaseName);

            _testsCollection = mongoDatabase.GetCollection<Test>(beFitDatabaseSettings.Value.TestsCollectionName);
            _attemptsCollection = mongoDatabase.GetCollection<Attempt>(beFitDatabaseSettings.Value.AttemptsCollectionName);
            _categoriesService = categoriesService;
        }
        public async Task<List<Test>> GetAsync()
        {
            return await _testsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Test?> GetAsync(string id)
        {
            return await _testsCollection.Find(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(string name, string categoryId, string description, string unit, ComparisonType comparisonType, double? lowerBound, double? higherBound)
        {
            var category = await _categoriesService.GetAsync(categoryId);
            switch (comparisonType)
            {
                case ComparisonType.HigherBetter:
                    higherBound = null;
                    break;
                case ComparisonType.LowerBetter:
                    lowerBound = null;
                    break;
                case ComparisonType.MiddleBetter:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Test.ComparisonType));
            }
            if (category == null)
            {
                throw new Exception("Category does not exist");
            }
            Test newTest = new Test() { Name = name, Category = category, ComparisonType = comparisonType, Description = description, Unit = unit };
            if(higherBound != null)
            {
                newTest.HigherBound = higherBound;
            }
            if(lowerBound != null)
            {
                newTest.LowerBound = lowerBound;
            }
            await _testsCollection.InsertOneAsync(newTest);
        }

        public async Task UpdateAsync(string id, string name, string categoryId, string description, string unit, ComparisonType comparisonType, double? lowerBound, double? higherBound)
        {
            var category = await _categoriesService.GetAsync(categoryId);
            switch (comparisonType)
            {
                case ComparisonType.HigherBetter:
                    higherBound = null;
                    break;
                case ComparisonType.LowerBetter:
                    lowerBound = null;
                    break;
                case ComparisonType.MiddleBetter:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Test.ComparisonType));
            }
            if (category == null)
            {
                throw new Exception("Category does not exist");
            }
            Test updatedTest = new Test() { Id = id, Name = name, Category = category, ComparisonType = comparisonType, Description = description, Unit = unit };
            if (higherBound != null)
            {
                updatedTest.HigherBound = higherBound;
            }
            if (lowerBound != null)
            {
                updatedTest.LowerBound = lowerBound;
            }
            await _testsCollection.ReplaceOneAsync(c => c.Id == id, updatedTest);

            var filter = Builders<Attempt>.Filter.Eq(a => a.Test.Id, id);
            var update = Builders<Attempt>.Update.Set(a => a.Test, updatedTest);
            await _attemptsCollection.UpdateManyAsync(filter, update);
        }

        public async Task RemoveAsync(string id)
        {
            await _testsCollection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
