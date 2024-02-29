using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BeFit.MongoDb.Api.Models
{
    public class AttemptLectorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("FamilyName")]
        public string FamilyName { get; set; }
    }
}
