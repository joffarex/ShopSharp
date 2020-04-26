using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSharp.Domain.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")] public decimal Value { get; set; }
    }
}