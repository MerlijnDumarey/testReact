using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeFit.MongoDb.Api.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;
        private readonly IMongoCollection<Test> _testsCollection;

        public CategoriesService(IOptions<BeFitDatabaseSettings> beFitDatabaseSettings)
        {
            var mongoClient = new MongoClient(beFitDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(beFitDatabaseSettings.Value.DatabaseName);

            _categoriesCollection = mongoDatabase.GetCollection<Category>(beFitDatabaseSettings.Value.CategoriesCollectionName);
            _testsCollection = mongoDatabase.GetCollection<Test>(beFitDatabaseSettings.Value.TestsCollectionName);
        }

        public async Task<List<Category>> GetAsync()
        {
            return await _categoriesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Category?> GetAsync(string id)
        {
            return await _categoriesCollection.Find(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Category newCategory)
        {
            await _categoriesCollection.InsertOneAsync(newCategory);
        }

        public async Task UpdateAsync(string id, Category updatedCategory)
        {
            await _categoriesCollection.ReplaceOneAsync(c => c.Id == id, updatedCategory);
            var filter = Builders<Test>.Filter.Eq(e => e.Category.Id, id);
            var update = Builders<Test>.Update.Set(e => e.Category, updatedCategory);
            await _testsCollection.UpdateManyAsync(filter, update);
        }

        public async Task RemoveAsync(string id)
        {
            await _categoriesCollection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
