using BeFit.MongoDb.Api.Validator;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class StudentCreateDto : BaseCreateDto
    {
        [Required]
        public string FamilyName { get; set; }
        [Required]
        [Range(1, 8)]
        public int CurrentSemester { get; set; }
        [Required]
        [StringLength(1)]
        [GenderAttribute]
        public string Gender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [RealisticDayOfBirth]
        public DateTime BirthDate { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
