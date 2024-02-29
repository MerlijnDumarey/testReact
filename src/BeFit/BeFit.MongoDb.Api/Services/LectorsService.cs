using BeFit.MongoDb.Api.Models;
using BeFit.MongoDb.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeFit.MongoDb.Api.Services
{
    public class LectorsService : ILectorsService
    {
        private readonly IMongoCollection<Lector> _lectorsCollection;
        private readonly IMongoCollection<Attempt> _attemptsCollection;

        public LectorsService(IOptions<BeFitDatabaseSettings> beFitDatabaseSettings)
        {
            var mongoClient = new MongoClient(beFitDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(beFitDatabaseSettings.Value.DatabaseName);

            _lectorsCollection = mongoDatabase.GetCollection<Lector>(beFitDatabaseSettings.Value.LectorsCollectionName);
            _attemptsCollection = mongoDatabase.GetCollection<Attempt>(beFitDatabaseSettings.Value.AttemptsCollectionName);
        }
        public async Task<List<Lector>> GetAsync()
        {
            return await _lectorsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Lector?> GetAsync(string id)
        {
            return await _lectorsCollection.Find(c => c.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Lector newLector)
        {
            await _lectorsCollection.InsertOneAsync(newLector);
        }

        public async Task UpdateAsync(string id, Lector updatedLector)
        {
            await _lectorsCollection.ReplaceOneAsync(c => c.Id == id, updatedLector);
            var updatedLectorModel = new AttemptLectorModel()
            {
                Id = updatedLector.Id,
                FamilyName = updatedLector.FamilyName,
                Name = updatedLector.Name,
            };
            var filter = Builders<Attempt>.Filter.Eq(a => a.Lector.Id, id);
            var update = Builders<Attempt>.Update.Set(a => a.Lector, updatedLectorModel);
            await _attemptsCollection.UpdateManyAsync(filter, update);
        }

        public async Task RemoveAsync(string id)
        {
            await _lectorsCollection.DeleteOneAsync(c => c.Id == id);
        }
    }
}
