namespace _3lab_komanda32.Models
{
    public class ManagePrivilege
    {
        public long BusinessId { get; set; }
        public long EmployeeId { get; set; }
        public string Privilege { get; set; } = string.Empty;
    }
}
