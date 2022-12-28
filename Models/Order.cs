using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class Order
    {
        //different in "DropDatabase" api and QuickFix document
        [Key]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        //public List<Product> Products { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public long PaymentId { get; set; }
        //instead of AdditionalInformationFromCustomer
        public string? Comment { get; set; }
        public Address? DeliveryAddress { get; set; }
        public List<Product> Products { get; set; }

        public Order()
        {
            Products = new List<Product>();
        }

    }
}
