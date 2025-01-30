namespace Application.Dtos.Request
{
    public class ServiceRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Velocity { get; set; }
        public decimal Price { get; set; }
    }
}
