using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class ProductDiscount
    {
        [Key]
        public int Id { get; set; }
        public double Discount { get; set; }
        public DateTime DiscountStart { get; set; }
        public DateTime DiscountEnd { get; set; }
    }
}
