using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeFit.MongoDb.Api.Models
{
    public class Attempt
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string? Id { get; set; }
        [BsonElement("Score")]
        public double Score { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonElement("Date")]
        public DateTime Date { get; set; }
        [BsonElement("Lector")]
        public AttemptLectorModel Lector { get; set; }
        [BsonElement("Student")]
        public AttemptStudentModel Student { get; set; }
        [BsonElement("Test")]
        public Test Test { get; set; }

    }
}
