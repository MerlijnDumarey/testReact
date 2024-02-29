namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class AttemptsGetByIdDto
    {
        public string Id { get; set; }
        public BasePersonDto Student { get; set; }
        public BasePersonDto Lector { get; set; }
        public BaseDto Test { get; set; }
        public string TestDescription { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }
    }
}
