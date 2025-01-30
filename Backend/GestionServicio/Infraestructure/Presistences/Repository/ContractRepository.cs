using Domain.Entities;
using Infraestructure.Presistences.Contexts;
using Infraestructure.Presistences.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Presistences.Repository
{
    internal class ContractRepository : GenericRepository<Contract>, IContractRepository
    {
        private readonly GestionServicesContext _context;
        public ContractRepository(GestionServicesContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Contract> GetContractByIdAsync(int id)
        {
            var response = await GetEntityQuery(contract => contract.Contractid == id)
                .Include(contract => contract.ServiceService)
                .Include(contract => contract.ClientClient)
                .Include(contract => contract.StatuscontractStatus)
                .Include(contract => contract.MethodpaymentMethodpayment)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return response!;
        }

        public async Task<Contract> GetListContractByClientIdAsync(int ClientId)
        {
            var contract = await GetEntityQuery(contract => contract.Datedelete == null && contract.ClientClientid == ClientId)
                .Include(contract => contract.ServiceService)
                .Include(contract => contract.ClientClient)
                .Include(contract => contract.StatuscontractStatus)
                .Include(contract => contract.MethodpaymentMethodpayment)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return contract!;
        }

        public async Task<bool> UpdateStatuesContract(int contractId, int stateId)
        {
            var contract = await GetEntityQuery(cont => cont.Contractid.Equals(contractId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (contract is null)
                return false;
            contract.StatuscontractStatusid = stateId;
            var result = await SaveAsync(contract);
            return result;
        }

        public async Task<bool> UpgradeService(int contractId, int serviceId, int stateId)
        {
            var contractOld = await GetEntityQuery(cont => cont.Contractid.Equals(contractId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (contractOld is null)
                return false;
            var service = await _context.Services.FindAsync(serviceId);
            if (service is null)
                return false;
            var contractNew = new Contract
            {
                Startdate = DateTimeOffset.Now,
                Enddate = contractOld.Enddate,
                ClientClientid = contractOld.ClientClientid,
                ServiceServiceid = service.Serviceid,
                StatuscontractStatusid = 1,
                MethodpaymentMethodpaymentid = contractOld.MethodpaymentMethodpaymentid,
                Datecreation = DateTimeOffset.Now
            };
            await SaveAsync(contractNew);
            await UpdateAsync(contractOld);
            return false;
        }
    }
}
