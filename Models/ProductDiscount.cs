namespace _3lab_komanda32.Models
{
    public class ProductDiscount
    {
        public int Id { get; set; }
        public double Discount { get; set; }
        public DateOnly DiscountStart { get; set; }
        public DateOnly DiscountEnd { get; set; }
    }
}
