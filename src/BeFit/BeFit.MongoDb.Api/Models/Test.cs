using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BeFit.MongoDb.Api.Models
{
    public class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Category")]
        public Category Category { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Unit")]
        public string Unit { get; set; }
        [BsonElement("ComparisonType")]
        public ComparisonType ComparisonType { get; set; }
        [BsonElement("LowerBound")]
        public double? LowerBound { get; set; }
        [BsonElement("HigherBound")]
        public double? HigherBound { get; set; }
    }
}
