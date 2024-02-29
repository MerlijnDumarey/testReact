using BeFit.MongoDb.Api.Validator;
using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class LectorsCreateDto : BaseCreateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RealisticDayOfBirth]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string FamilyName { get; set; }
    }
}
