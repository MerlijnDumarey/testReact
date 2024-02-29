using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class AttemptUpdateDto : AttemptCreateDto
    {
        [Required]
        [StringLength(24)]
        public string Id { get; set; }
    }
}
