namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class SearchAttemptsByLectorIdDto
    {
        public IEnumerable<AttemptsGetByIdDto> Items { get; set; }
    }
}
