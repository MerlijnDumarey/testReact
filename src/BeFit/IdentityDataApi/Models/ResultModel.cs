namespace IdentityDataApi.Models
{
    public class ResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public List<T> Items { get; set; }
    }
}
