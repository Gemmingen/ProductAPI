using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public required string Name { get; set; }

        [Range(0.01, 10000000)]
        public decimal Price { get; set; }

        public required string Category { get ; set;}
    }
}
