namespace Application.Dtos.Response
{
    public class ContractResponse
    {
        public int Contractid { get; set; }
        public DateTimeOffset Startdate { get; set; }
        public DateTimeOffset Enddate { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDesciption { get; set; }
        public string? StatusContract { get; set; }
        public string? NameClient { get; set; }
        public string? IdetificationClient { get; set; }
        public string? MethPayment { get; set; }
    }
}
