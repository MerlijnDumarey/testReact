using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BeFit.MongoDb.Api.Models
{
    public class Student : ApplicationUser
    {
        [BsonElement("Gender")]
        public string Gender { get; set; }
        [BsonElement("CurrentSemester")]
        public int CurrentSemester { get; set; }
        [BsonElement("Weight")]
        public double Weight { get; set; } = 70;
        [BsonElement("Height")]
        public int Height { get; set; } = 160;
        [BsonElement("HasEvaluationPermission")]
        public bool HasEvaluationPermission { get; set; }
    }
}
