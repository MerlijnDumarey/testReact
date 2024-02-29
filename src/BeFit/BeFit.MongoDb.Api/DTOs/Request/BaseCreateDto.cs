using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class BaseCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
