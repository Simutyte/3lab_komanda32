using _3lab_komanda32.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class OrderConfirmation
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }
        public long EmployeeId { get; set; }
        public bool Refundable { get; set; }
        public DeliveryOptions DeliveryOption { get; set; }
        public string? Comment { get; set; }
    }
}
