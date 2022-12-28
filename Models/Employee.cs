using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public long SubunitId { get; set; }
    }
}
