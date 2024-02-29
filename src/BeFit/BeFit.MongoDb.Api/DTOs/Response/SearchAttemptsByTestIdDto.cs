namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class SearchAttemptsByTestIdDto
    {
        public IEnumerable<AttemptsGetByIdDto> Attempts { get; set; }
    }
}
