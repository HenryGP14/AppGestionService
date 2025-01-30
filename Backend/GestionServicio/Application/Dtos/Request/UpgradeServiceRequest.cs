namespace Application.Dtos.Request
{
    public class UpgradeServiceRequest
    {
        public int ContractId { get; set; }
        public int ServiceId { get; set; }
        public int StateContractId { get; set; }
    }
}
