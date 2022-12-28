namespace _3lab_komanda32.Models
{
    public class Order
    {
        //different in "DropDatabase" api and QuickFix document
        public long Id { get; set; }
        public long CustomerId { get; set; }
        //public List<Product> Products { get; set; }
        public TimeSpan OrderDate { get; set; }
        public TimeSpan CompletionDate { get; set; }
        public long PaymentId { get; set; }
        //instead of AdditionalInformationFromCustomer
        public string? Comment { get; set; }
        public Address? DeliveryAddress { get; set; }
        public List<Product> Products { get; set; }
    }
}
