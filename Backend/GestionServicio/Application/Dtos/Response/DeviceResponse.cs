namespace Application.Dtos.Response
{
    public class DeviceResponse
    {
        public int DeviceId { get; set; }
        public string Devicename { get; set; } = null!;
        public DateTimeOffset Datecreation { get; set; }
    }
}
