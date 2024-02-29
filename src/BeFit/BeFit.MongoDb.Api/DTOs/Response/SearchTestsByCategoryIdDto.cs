namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class SearchTestsByCategoryIdDto
    {
        public IEnumerable<TestsGetByIdDto> Tests { get; set; }
    }
}
