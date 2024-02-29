namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class AttemptsGetAllDto 
    {
        public IEnumerable<AttemptsGetByIdDto> Items { get; set; }
    }
}
