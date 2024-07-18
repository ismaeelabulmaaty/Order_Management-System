using System.ComponentModel.DataAnnotations;

namespace Order_Management.DTOS
{
    public class ProductRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
