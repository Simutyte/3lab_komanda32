using _3lab_komanda32.Models.Enums;

namespace _3lab_komanda32.Models
{
    public class Payment
    { 
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ReservationId { get; set; }
        public long SubunitID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Gratuity { get; set; }
        public PaymentStatus Status { get; set; }
        public double LoyaltyDiscount { get; set; }
        public decimal TotalDiscountsApplied { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
    }
}
