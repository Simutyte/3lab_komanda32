namespace _3lab_komanda32.Models
{
    public class Address
    {
        public long Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
    }
}
