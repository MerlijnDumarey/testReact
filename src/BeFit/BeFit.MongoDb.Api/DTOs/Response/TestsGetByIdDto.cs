using BeFit.MongoDb.Api.Models;

namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class TestsGetByIdDto : BaseDto
    {
        public BaseDto Category { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public ComparisonType ComparisonType { get; set; }
        public double? LowerBound { get; set; }
        public double? HigherBound { get; set; }
    }
}
