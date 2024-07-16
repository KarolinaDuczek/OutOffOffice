namespace OutOfOffice_web.Models
{
    public class PeoplePartner
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public virtual ICollection<Employee> PartnerEmployees { get; set; } = new List<Employee>();
    }
}