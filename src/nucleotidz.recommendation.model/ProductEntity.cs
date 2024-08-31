using System.ComponentModel.DataAnnotations;

namespace nucleotidz.recommendation.model
{
    public class ProductEntity
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
