using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class LectorsUpdateDto : LectorsCreateDto
    {
        [Required]
        public string Id { get; set; }
    }
}
