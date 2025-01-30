namespace Infraestructure.Presistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IAttentionRepository Attention { get; }
        IServiceRepository Service { get; }
        IContractRepository Contract { get; }
        IPaymentRepository Payment { get; }
        IClientRepository Client { get; }
        IUsercashRepository Usercash { get; }
        ICashRepository Cash { get; }
        ITurnRepository Turn { get; }
        IDeviceRepository Device { get; }
        IErrorsRepository Errors { get; }
        IMethodpaymentRepository Methodpayment { get; }
        IAttentionstatusRepository Attentionstatus { get; }
        IAttentiontypeRepository Attentiontype { get; }
        IRolRepository Rol { get; }
        IUserstatusRepository Userstatus { get; }
        IStatuscontractRepository Statuscontract { get; }
        Task saveChangesAsync();
        void saveChanges();
    }
}
