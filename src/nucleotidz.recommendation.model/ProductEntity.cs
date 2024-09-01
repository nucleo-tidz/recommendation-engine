using System.ComponentModel.DataAnnotations;

namespace nucleotidz.recommendation.model
{
    public class ProductEntity
    {
        [Required]
        public required string Code { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
