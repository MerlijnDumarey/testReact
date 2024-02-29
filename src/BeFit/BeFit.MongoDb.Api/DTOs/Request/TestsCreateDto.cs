using BeFit.MongoDb.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace BeFit.MongoDb.Api.DTOs.Request
{
    public class TestsCreateDto : BaseCreateDto
    {
        [Required]
        [StringLength(24, MinimumLength = 24)]
        public string CategoryId { get; set; }
        [Required]
        [StringLength(1500)]
        public string Description { get; set; }
        [Required]
        [StringLength(1000)]
        public string Unit { get; set; }
        [Required]
        public ComparisonType ComparisonType { get; set; }
        public double? LowerBound { get; set; }
        public double? HigherBound { get; set; }
    }
}
