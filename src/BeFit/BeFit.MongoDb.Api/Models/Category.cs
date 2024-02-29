using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeFit.MongoDb.Api.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
