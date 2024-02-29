namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class BaseGetAllDto<T> where T : BaseDto
    {
        public IEnumerable<T> Items { get; set; }
    }
}
