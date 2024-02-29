using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class StudentUpdateDto : StudentCreateDto
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string Id { get; set; }
        [Required]
        [Range(30, 300)]
        public double Weight { get; set; }
        [Required]
        [Range(1, 300)]
        public int Height { get; set; }
        public bool HasEvaluationPermission { get; set; }
    }
}
