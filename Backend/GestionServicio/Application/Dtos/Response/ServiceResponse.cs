namespace Application.Dtos.Response
{
    public class ServiceResponse
    {
        public int Serviceid { get; set; }
        public string? Servicename { get; set; }
        public string? Servicedescription { get; set; }
        public decimal? Price { get; set; }
    }
}
