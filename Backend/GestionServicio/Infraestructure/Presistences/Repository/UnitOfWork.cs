using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;

namespace Infraestructure.Presistences.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GestionServicesContext _context;
        public IUserRepository User { get; private set; }

        public IAttentionRepository Attention { get; private set; }

        public IServiceRepository Service { get; private set; }

        public IContractRepository Contract { get; private set; }

        public IPaymentRepository Payment { get; private set; }

        public IClientRepository Client { get; private set; }

        public IUsercashRepository Usercash { get; private set; }

        public ICashRepository Cash { get; private set; }

        public ITurnRepository Turn { get; private set; }

        public IDeviceRepository Device { get; private set; }

        public IErrorsRepository Errors { get; private set; }

        public IMethodpaymentRepository Methodpayment { get; private set; }

        public IAttentionstatusRepository Attentionstatus { get; private set; }

        public IAttentiontypeRepository Attentiontype { get; private set; }

        public IRolRepository Rol { get; private set; }

        public IUserstatusRepository Userstatus { get; private set; }

        public IStatuscontractRepository Statuscontract { get; private set; }

        public UnitOfWork(GestionServicesContext context)
        {
            _context = context;
            User = new UserRepository(_context);
            Attention = new AttentionRepository(_context);
            Service = new ServiceRepository(_context);
            Contract = new ContractRepository(_context);
            Payment = new PaymentRepository(_context);
            Client = new ClientRepository(_context);
            Usercash = new UsercashRepository(_context);
            Cash = new CashRepository(_context);
            Turn = new TurnRepository(_context);
            Device = new DeviceRepository(_context);
            Errors = new ErrorsRepository(_context);
            Methodpayment = new MethodpaymentRepository(_context);
            Attentionstatus = new AttentionstatusRepository(_context);
            Attentiontype = new AttentiontypeRepository(_context);
            Rol = new RolRepository(_context);
            Userstatus = new UserstatusRepository(_context);
            Statuscontract = new StatuscontractRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }

        public async Task saveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
