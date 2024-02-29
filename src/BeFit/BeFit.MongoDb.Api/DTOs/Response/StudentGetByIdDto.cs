namespace BeFit.MongoDb.Api.DTOs.Response
{
    public class StudentGetByIdDto : BaseDto
    {
        public string Gender { get; set; }
        public int CurrentSemester { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public double? Weight { get; set; }
        public int? Height { get; set; }
        public string FamilyName { get; set; }
        public bool? HasEvaluationPermission { get; set; }
    }
}
