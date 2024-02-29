namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class SearchAttemptsByStudentIdDto
    {
        public IEnumerable<AttemptsGetByIdDto> Items { get; set; }
    }
}
