namespace Backend.DTO
{
    public class CompanyAddress
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<CompanyTelephone> CompanyTelephones { get; set; }
    }
}
