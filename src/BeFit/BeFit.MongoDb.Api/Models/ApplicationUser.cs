using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BeFit.MongoDb.Api.Models
{
    public class ApplicationUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("FamilyName")]
        public string FamilyName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("Dob")]
        public DateTime Dob { get; set; }
    }
}
