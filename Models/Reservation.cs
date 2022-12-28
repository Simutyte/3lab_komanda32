using _3lab_komanda32.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class Reservation
    {
        [Key]
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public Employee? Employee { get; set; }
        public List<Service> Services { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public long SubunitId { get; set; }
        public long PaymentId { get; set; }
        //instead of AdditionalInformationFromCustomer
        public string? Comment { get; set; }
        public ReservationStatus Status { get; set; }
        public Address DeliveryAddress { get; set; }

        public Reservation()
        {
            Services = new List<Service>();
            DeliveryAddress = new Address();
        }
    }
}
