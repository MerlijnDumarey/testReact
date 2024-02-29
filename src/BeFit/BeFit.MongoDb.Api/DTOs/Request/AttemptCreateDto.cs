using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class AttemptCreateDto
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string TestId { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string StudentId { get; set; }
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string LectorId { get; set; }
        [Required]
        public double Score { get; set; }
        public DateTime Date { get; set; }
    }
}
