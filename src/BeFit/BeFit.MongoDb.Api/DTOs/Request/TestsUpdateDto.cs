using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class TestsUpdateDto : TestsCreateDto
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string Id { get; set; }
    }
}
