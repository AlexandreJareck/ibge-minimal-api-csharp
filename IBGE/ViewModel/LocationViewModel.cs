namespace IBGE.DTO
{
    public record LocationViewModel
    {
        public string Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
}