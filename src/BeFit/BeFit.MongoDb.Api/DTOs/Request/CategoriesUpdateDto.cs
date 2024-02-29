using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class CategoriesUpdateDto : CategoriesCreateDto
    {
        [Required]
        public string Id { get; set; }
    }
}
