using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class LoyaltyProgram
    {   
        [Key]
        public long CustomerId { get; set; }
        public DateTime LoyaltyStart { get; set; }

        public LoyaltyProgram() { }
        public LoyaltyProgram(long custId)
        {
            CustomerId = custId;
            LoyaltyStart = DateTime.Now;
        }
    }
}
