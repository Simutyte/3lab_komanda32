using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class Business
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long OwnerId { get; set; }

        public Address Address { get; set; }

        public Business() 
        { 
            Address = new Address();
        }
    }
}
