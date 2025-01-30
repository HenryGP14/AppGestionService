namespace Application.Dtos.Request
{
    public class ContractRequest
    {
        public DateTimeOffset Startdate { get; set; }
        public DateTimeOffset Enddate { get; set; }
        public int ServiceServiceid { get; set; }
        public int StatuscontractStatusid { get; set; }
        public int ClientClientid { get; set; }
        public int MethodpaymentMethodpaymentid { get; set; }
    }
}
