namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class LectorsGetByIdDto : BaseDto
    {
        public string Email { get; set; }
        public string FamilyName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
